using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        public static Dictionary<int, List<string>> NodeKeyAtLevels = new Dictionary<int, List<string>>();
        public static Dictionary<string, string> childParent = new Dictionary<string, string>();
        public static Dictionary<string, string> diffList = new Dictionary<string, string>();
        public static List<KeyValuePair<string, string>> sourceDiff = new List<KeyValuePair<string, string>>();
        public static List<KeyValuePair<string, string>> destDiff = new List<KeyValuePair<string, string>>();
        //public static string line;
        public static List<XElement> elementsToBeIgnoredOriginal = new List<XElement>();
        //public  static string Hiearchy;
        public static List<KeyValuePair<XElement, bool>> elementsToBeIgnoredFormatted = new List<KeyValuePair<XElement, bool>>();
        public static List<string> elementsToBeIgnoredOriginalNames = new List<string>();
        static void Main(string[] args)
        {
            try
            {

                Console.WriteLine("Enter Source Path");
                var masterFile = Console.ReadLine();

                Console.WriteLine("Enter Target Path");
                var childDir = Console.ReadLine();


                //var masterFile = "C:\\Users\\schaturvedi\\Downloads\\pk3.xml";
                //var childDir = "C:\\Users\\schaturvedi\\Downloads\\invs\\";
                var hiearchyFile = "C:\\Users\\schaturvedi\\Downloads\\hiearchy.txt";
                var hXDOC = GetXDOC(GetSortedXML(hiearchyFile));
                elementsToBeIgnoredOriginal = hXDOC.Root.Elements().ToList();

                var master = GetSortedXML(masterFile);
                var masterxDoc = GetXDOC(master);
                //  var hh = hi.Root.Descendants("InvoiceHeaderGroup").ToList();
                var masterInvoices = masterxDoc.Root.Descendants("InvoiceHeaderGroup").ToDictionary(x => x.Element("InvoiceNumber").Value, x => x);
                //var masterInvoices = masterxDoc.Root.Descendants("InvoiceHeaderGroup").ToList<KeyValuePair<string , string>>(x => x.Element("InvoiceNumber").Value, x => x);

                foreach (var e in elementsToBeIgnoredOriginal)
                {
                    if (!e.HasElements)//It means It's a hiearchy
                        elementsToBeIgnoredFormatted.Add(new KeyValuePair<XElement, bool>(e, false));
                    else
                        elementsToBeIgnoredFormatted.Add(new KeyValuePair<XElement, bool>(e, true));

                }
                elementsToBeIgnoredOriginalNames = elementsToBeIgnoredFormatted.Select(x => x.Key.Name.LocalName).ToList();
                string[] sourceFiles = Directory.GetFiles(childDir, "*.xml");
                string[] childfiles = sourceFiles.Distinct().ToArray();
                Dictionary<string, XElement> childInvoices = new Dictionary<string, XElement>();

                foreach(var f in childfiles)
                {
                    var cf = GetXDOC(GetSortedXML(f)).Root.Descendants("InvoiceHeaderGroup");
                    var iNumber = cf.Select(y => y.Element("InvoiceNumber").Value).First();
                    if (!childInvoices.ContainsKey(iNumber))
                    {
                        childInvoices.Add(iNumber, cf.First());
                    }
                }
                var masterInvoiceNumberList = masterInvoices.Keys.Union(childInvoices.Keys).ToList();
                var baseDir = @"c:\DiffResult\";
                var resultDir = baseDir;
                if (!Directory.Exists(resultDir))
                    Directory.CreateDirectory(resultDir);
                List<string> availableInMasterOnly = masterInvoices.Keys.Except(childInvoices.Keys).ToList();
                availableInMasterOnly.ForEach(x => masterInvoices.Remove(x));
                //available in target missing in source
                List<string> availableInSourceOnly = childInvoices.Keys.Except(masterInvoices.Keys).ToList();
                availableInSourceOnly.ForEach(x => childInvoices.Remove(x));
                resultDir = baseDir + "\\AbsenceList\\";
                if (!Directory.Exists(resultDir))
                    Directory.CreateDirectory(resultDir);
                using (StreamWriter s = new StreamWriter(resultDir + "Absence.txt"))
                {
                    s.WriteLine("List of Invoices available in master xml but unavailable in individual xmls.");
                    availableInMasterOnly.ForEach(x => s.WriteLine(x));
                    availableInMasterOnly.ForEach(x => masterInvoiceNumberList.Remove(x));
                    s.WriteLine("/----------------------------------------------------------------------/");
                    s.WriteLine("List of Invoices available in individual xmls but unavailable in master xml.");
                    availableInSourceOnly.ForEach(x => s.WriteLine(x));
                    availableInSourceOnly.ForEach(x => masterInvoiceNumberList.Remove(x));

                }


                
                List<string> identicalInvoices = new List<string>();

                resultDir = baseDir + "\\DifferenceDir\\";

                if (!Directory.Exists(resultDir))
                    Directory.CreateDirectory(resultDir);
                string fileName = resultDir + "name" + ".csv";
                //File.Delete(fileName);
                foreach (var i in masterInvoiceNumberList)
                {
                    var filens = sourceFiles.Where(x => x.Contains(i)).FirstOrDefault();
                    //if (filens != null)
                    //{
                        var filen = Path.GetFileNameWithoutExtension(filens);
                    //}
                    string result = string.Empty;
                    //string result;
                    var masterContent = masterInvoices.ContainsKey(i) ? masterInvoices[i] : null;
                    var childContent = childInvoices.ContainsKey(i) ? childInvoices[i] : null;
                    if (masterContent != null && childContent != null)
                    {

                        try
                        {
                            result = GetDiffString(masterContent, childContent);
                        }
                        catch (Exception e)
                        {

                            result = e.GetBaseException()?.Message;
                        }
                        //File.WriteAllText(fileName, result);
                        //using (StreamWriter writer = new StreamWriter(fileName))
                        //{
                        //   // writer.WriteLine(result);
                        //    writer.WriteLine(Environment.NewLine);
                        //}
                       
                    }
                    //while(result.Count>0)
                    if (string.IsNullOrWhiteSpace(result?.Trim()))
                        identicalInvoices.Add(i);
                    else
                    {
                        result = "FileName" + "-" + filen + Environment.NewLine + "InvoiceNumber" + "-" + i + Environment.NewLine + result;
                        result = Regex.Replace(result, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
                        //if(result!=null)
                        //File.WriteAllText(fileName, result);
                        //File.WriteAllText(fileName, result);
                        File.AppendAllText(fileName, result + Environment.NewLine);
                        //File.AppendText(fileName, result);
                    }




                }
                resultDir = baseDir + "\\Identical\\";
                if (!Directory.Exists(resultDir))
                    Directory.CreateDirectory(resultDir);
                using (StreamWriter s = new StreamWriter(resultDir + "Identical.txt"))
                {
                    s.WriteLine("List of Invoices which are identical in master xml and in individual xmls.");
                    identicalInvoices.ForEach(x => s.WriteLine(x));
                    
                }



            }
            catch (Exception e) 
            { 

            }
        }


        private static void SortXml(XContainer parent)
        {
            var elements = parent.Elements()
                .OrderBy(e => e.Name.LocalName)
                .ThenBy(e => (string)e.Attribute("name"))
                .ToArray();

            Array.ForEach(elements, e => e.Remove());

            foreach (var element in elements)
            {
                if (elementsToBeIgnoredOriginalNames.Contains(element.Name.LocalName))
                {
                    var formattedEle = elementsToBeIgnoredFormatted.Where(x => x.Key.Name.LocalName == element.Name.LocalName).FirstOrDefault();
                
                
                    if (!XElement.DeepEquals(SortElement(element),SortElement(formattedEle.Key)))
                    {
                        parent.Add(element);
                        SortXml(element);
                    }

                }
                else
                {

                    parent.Add(element);
                    SortXml(element);
                }

            }
        }

        private static XElement SortElement(XElement parent)
        {
            var elements = parent.Elements()
                .OrderBy(e => e.Name.LocalName)
                .ThenBy(e => (string)e.Attribute("name"))
                .ToArray();

            Array.ForEach(elements, e => e.Remove());

            foreach (var element in elements)
            {
               

                    parent.Add(element);
                    SortXml(element);

            }
            return parent;
        }


        private static string GetDiffString(XElement sourceFile, XElement fileToBeComapred)
        {
            //var Hiearchy = "C:\\Users\\schaturvedi\\Downloads\\hiearchy.txt";
            //var h = GetSortedXML(Hiearchy);
            //var hi = GetXDOC(h);
            //var hh = hi.Root.Descendants("InvoiceHeaderGroup").ToList();

            sourceDiff.Clear();
            destDiff.Clear();
            var diifList = GetDifference(sourceFile, fileToBeComapred);
            List<Tuple<string, List<string>, List<string>>> finalRes = new List<Tuple<string, List<string>, List<string>>>();
            var formatedDiff1 = sourceDiff.GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(y => y.Value).ToList());//).ToList();
            var formatedDiff2 = destDiff.GroupBy(x => x.Key).ToDictionary(x => x.Key, x => x.Select(y => y.Value).ToList());//).ToList();
            var availableInSourceOnly = formatedDiff1.Keys.Except(formatedDiff2.Keys);
            var availableInDestOnly = formatedDiff2.Keys.Except(formatedDiff1.Keys);
            var common = formatedDiff1.Keys.Intersect(formatedDiff2.Keys);
            foreach (var s in availableInSourceOnly)
            {
                finalRes.Add(new Tuple<string, List<string>, List<string>>(s, formatedDiff1[s], new List<string>()));
            }
            foreach (var c in common)
            {
                finalRes.Add(new Tuple<string, List<string>, List<string>>(c, formatedDiff1[c], formatedDiff2[c]));
            }

            foreach (var s in availableInDestOnly)
            {
                finalRes.Add(new Tuple<string, List<string>, List<string>>(s, new List<string>(), formatedDiff2[s]));
            }
            string resultString = string.Empty;

            resultString = "InvoiceNumber" + "," + "Node" + "," + "Source file values" + "," + "Target file values" + Environment.NewLine;
            
            foreach (var e in finalRes)
            {
                //var xt = e.Item2.Distinct().ToList();
                //var ct = e.Item3.Distinct().ToList();
                //if (line.Contains(e.Item1) && line.Contains(xt[0]) && line.Contains(ct[0]))
                //    continue;
                

                var sEnum = e.Item2.GetEnumerator();
                var dEnum = e.Item3.GetEnumerator();
                int count = 0;
                if (e.Item3.Count > e.Item2.Count)
                {
                    foreach (var e1 in e.Item3)
                    {
                        sEnum.MoveNext();
                        dEnum.MoveNext();
                        if (count == 0)
                            resultString += sourceFile.Element("InvoiceNumber").Value + "," + e.Item1 + "," + sEnum.Current + "," + dEnum.Current + Environment.NewLine;
                        else
                            resultString += sourceFile.Element("InvoiceNumber").Value + ","+"" +","+ "Null" + sEnum.Current + "," + dEnum.Current + Environment.NewLine;

                        count++;
                    }

                }
                else
                {
                    foreach (var e1 in e.Item2)
                    {
                        sEnum.MoveNext();
                        dEnum.MoveNext();
                        if (count == 0)
                            resultString +=    sourceFile.Element("InvoiceNumber").Value  +  ","  + e.Item1 + "," + sEnum.Current + "," + dEnum.Current + Environment.NewLine;
                        else
                            resultString +=     sourceFile.Element("InvoiceNumber").Value + "," + ""+","+"Null" + "," + sEnum.Current + "," + dEnum.Current + Environment.NewLine;

                        //resultString =  "invoiceNumber" + sourceFile.Element("InvoiceNumber").Value  + resultString; 
                        count++;
                    }
                }
            }
            return resultString;
        }

        private static string GetSortedXML(string filePath)
        {
            XDocument xDoc = XDocument.Load(filePath);
            if (xDoc.Root != null)
                SortXml(xDoc.Root);
            return xDoc.ToString();
        }
        private static XDocument GetXDOC(string xml)
        {
            XDocument xDoc = XDocument.Parse(xml);
            return xDoc;
        }
        static List<KeyValuePair<string, string>> GetDifference(XElement sourceFile, XElement fileToBeComapred)
        {
            var c = sourceFile.Descendants().ToList();
            var sourceDescendents = sourceFile.Descendants().Where(x => !x.HasElements).Select(x => new KeyValuePair<string, string>(x.Name.ToString(), x.Value.ToString())).ToList();
            var fileToBeComapredDescendents = fileToBeComapred.Descendants().Where(x => !x.HasElements).Select(x => new KeyValuePair<string, string>(x.Name.ToString(), x.Value.ToString())).ToList();
            var equalProps = (from e1 in fileToBeComapredDescendents
                              join e2 in sourceDescendents
                              on new { a1 = e1.Key, a2 = e1.Value } equals new { a1 = e2.Key, a2 = e2.Value }
                              select new KeyValuePair<string, string>(e1.Key, e1.Value)).ToList();
            equalProps.ForEach(x => fileToBeComapredDescendents.Remove(x));
            equalProps.ForEach(x => sourceDescendents.Remove(x));
            sourceDiff = sourceDescendents;
            destDiff = fileToBeComapredDescendents;
            //fileToBeComapredDescendents.ForEach(x => )
            //for(int i=0;i<fileToBeComapredDescendents.Count;i++)
            //{
             //if(line.Contains(fileToBeComapredDescendents[i].Key) || line.Contains(fileToBeComapredDescendents[i].Value))
               // {
             //       fileToBeComapredDescendents.RemoveAt(i);
             //   }
                    
           // }
            return fileToBeComapredDescendents;
        }

    }
}
