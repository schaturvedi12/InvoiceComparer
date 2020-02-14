using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.Xml;
using System.Data;
using Microsoft.XmlDiffPatch;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;
using System.Collections;

namespace ConsoleApp2
{
                  
    class parse
    {

        public static List<string> elementList = new List<string>();
        public static string line;
        public static List<KeyValuePair<string, List<KeyValuePair<string, List<string>>>>> result =
            new List<KeyValuePair<string, List<KeyValuePair<string, List<string>>>>>();
        //var FinalResult = new Dictionary<string, List<string>>();
        // public InvoiceHeaderGroup h;

        public static List<KeyValuePair<string, List<string>>> final = new List<KeyValuePair<string, List<string>>>();

        static void ain(string[] args)
        {
            //Console.WriteLine("Enter Target Path");
            //string targetPath = Console.ReadLine();
            //Console.WriteLine("Enter Source Path");
            //string sourcePath = Console.ReadLine();
            //Console.WriteLine("Enter Result File Path");
            //string resultFilePath = Console.ReadLine();
            //Console.WriteLine("Enter the path for Hiearchy File");
            //string Hiearchy = Console.ReadLine();
            var path1 = "C:\\Users\\schaturvedi\\Downloads\\child.xml";
            XDocument doc1 = XDocument.Load(path1);
            var alle = doc1.Descendants();
            
           var file1 = Program.DeSerialize(path1);
            string sorted = Program.GetSortedXML(path1);
            string original = Program.GetOriginalXML(path1);
            Program.FillNodesAtKey(Program.GetXDOC(sorted).Root);

            string targetPath = @"C:\Users\schaturvedi\Downloads\Invoices (1) (1)\Invoices (1)";
            string sourcePath = @"C:\Users\schaturvedi\Downloads\810_20200131_032553";
            string resultFilePath = @"C:\Users\schaturvedi\Desktop\Test\Result.txt";
            string Hiearchy = @"C:\Users\schaturvedi\Downloads\hiearchy.txt";

            string[] sourceFile = Directory.GetFiles(sourcePath, "*.xml");
            //string[] HiearchyFile = Directory.GetFiles(HiearchyPath, "*.xml");
            //List<InvoiceHeaderGroup> Hir = new List<InvoiceHeaderGroup>();  
            var Source = DeSerialize(sourceFile[0]);
            //var Hiearchy = DeSerialize(HiearchyFile[0]);
            //Dictionary<string, string> pt = new Dictionary<string, string>();
            //string contents = File.ReadAllText(@"C:\Users\schaturvedi\Downloads\hiearchy.txt");
            //var lines = File.ReadLines(@"C:\Users\schaturvedi\Downloads\hiearchy.txt");
            //string[] vc = lines.ToArray();
            //vc = vc.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //List<string> temp = new List<string>();
            //foreach(var v in vc)
            //{
            //    if (v.Length > 3)
            //        temp.Add(v);
            //}
            //vc = temp.ToArray();
            //List<string[]> lineList = lines.ToList();

            //pt = File.ReadLines(@"C:\Users\schaturvedi\Downloads\hiearchy.txt").ToDictionary(k => k[0], v => v[1]);

            var SourceInvoices = Source?.InvoiceGroup?.ToDictionary(x => x.InvoiceHeaderGroup.InvoiceNumber, x => x.InvoiceHeaderGroup);
           //var HiearchyInvoices = Hiearchy?.InvoiceGroup?.ToDictionary(x => x.InvoiceHeaderGroup.InvoiceNumber, x => x.InvoiceHeaderGroup);
            //InvoiceHeaderGroup hire = Hiearchy.InvoiceGroup.
            //var HiearchyInvoices = Hiearchy?.InvoiceGroup?(x => x.InvoiceHeaderGroup)
        
            Dictionary<string, InvoiceHeaderGroup> TargetInvoices = new Dictionary<string, InvoiceHeaderGroup>();
            string[] targetFiles = Directory.GetFiles(targetPath, "*.xml");
            foreach (var t in targetFiles)
            {
                var TargetInvoice = DeSerialize(t);
                if(!TargetInvoices.ContainsKey(TargetInvoice.InvoiceGroup[0].InvoiceHeaderGroup.InvoiceNumber))
                {
                    TargetInvoices.Add(TargetInvoice.InvoiceGroup[0].InvoiceHeaderGroup.InvoiceNumber, TargetInvoice.InvoiceGroup[0].InvoiceHeaderGroup);

                }
                
            }
           // List<KeyValuePair<string, string>> res = new List<KeyValuePair<string, string>>();

            List<string> availableInTargetAbsetInSource = new List<string>();
            availableInTargetAbsetInSource = TargetInvoices.Keys.Except(SourceInvoices.Keys).ToList();


            List<string> availableInSourceAbsentInTarget = new List<string>();
            availableInSourceAbsentInTarget = SourceInvoices.Keys.Except(TargetInvoices.Keys).ToList();

            availableInTargetAbsetInSource.ForEach(x => TargetInvoices.Remove(x));
            availableInSourceAbsentInTarget.ForEach(x => SourceInvoices.Remove(x));

            using (StreamReader sr = new StreamReader(Hiearchy))
            {
                line = sr.ReadToEnd();
               
            }


                //Code=2 when Invoice is available in meta but not in target
                //List<string> availableInMasterAbsentInTarget = new List<string>();
                //availableInMasterAbsentInTarget = parentInvoices.Keys.Except(childInvoices.Keys).ToList();
                // res.AddRange(availableInMasterAbsentInTarget.Select(x => new KeyValuePair<string, string>(x, "Invoice is available in source but not in target")).ToList());
                //availableInMasterAbsentInTarget.ForEach(x => parentInvoices.Remove(x));
                //Code=3 when Invoice is available in target but not in meta
                //List<string> availableInTargetAbsentInMaster = new List<string>();
                //availableInTargetAbsentInMaster = childInvoices.Keys.Except(parentInvoices.Keys).ToList();
                //res.AddRange(availableInTargetAbsentInMaster.Select(x => new KeyValuePair<string, string>(x, "Invoice is available in target but not in source")).ToList());
                //availableInTargetAbsentInMaster.ForEach(x => childInvoices.Remove(x));




                foreach (KeyValuePair<string, InvoiceHeaderGroup> entry in TargetInvoices)
            {
                string key = entry.Key;
                InvoiceHeaderGroup value = entry.Value;

                InvoiceHeaderGroup par = SourceInvoices[key];
                //InvoiceHeaderGroup Hir = HiearchyInvoices[key;

                AddressGroup obj = new AddressGroup();
                InvoiceItemGroup IIG = new InvoiceItemGroup();
                LotGroup lt = new LotGroup();
                LotRecord lr = new LotRecord();
                InvoiceGroup ig = new InvoiceGroup();
                InvoiceHeaderGroup h = entry.Value;
                //foreach (KeyValuePair<string, InvoiceHeaderGroup> ent in HiearchyInvoices)
                //{
                //     h = ent.Value;
                //}

              
                {
                if (ig.Compare(par, value) != 0)
                    {
                        
                        //result.Insert(0, new KeyValuePair<string, List< KeyValuePair<string, List<string>>>>("<InvoiceGroup>", InvoiceHeaderGroup.ihg));
                        if(elementList.Count >0)
                        final.Insert(0, new KeyValuePair<string, List<string>>(key, elementList));
                        elementList = new List<string>();
                    }
                }
                //}
            }
       /*     using (StreamWriter writer = new StreamWriter(resultFilePath))

            {
                foreach (var x in result)
                {
                    writer.WriteLine(x);
                }

            }*/
            var dir = @"C:\Test\testing";  // folder location

            if (!Directory.Exists(dir))  // if it doesn't exist, create
                Directory.CreateDirectory(dir);

            // use Path.Combine to combine 2 strings to a path

            var filename = "Testing.txt";
            dir = System.IO.Path.Combine(dir, filename);

            File.Create(dir);

            /* using (StreamWriter writer = new StreamWriter(resultFilePath))
             {
                 Console.WriteLine("Invoices Present in Target but not in Source");
                 foreach(var invoice in availableInTargetAbsetInSource)
                 {
                     writer.WriteLine(invoice);
                 }
                 Console.WriteLine("\n");
             }

             using (StreamWriter writer = new StreamWriter(resultFilePath))
             {
                 Console.WriteLine("Invoices Present in Source not in Target");
                 foreach (var invoice in availableInSourceAbsentInTarget)
                 {
                     writer.WriteLine(invoice);
                 }
                 Console.WriteLine("\n");

             }*/

         

            using (StreamWriter writer = new StreamWriter(resultFilePath))

            {
                for (int i = 0; i < final.Count; i++)
                {
                    var rt = final[i].Value.Distinct().ToList();
                    writer.WriteLine(final[i].Key);
                    if (rt.Count > 1)
                    {
                        for (int y = 0; y < rt.Count; y++)
                        {
                            writer.WriteLine(rt[y]);
                        }
                    }
                    else
                   {
                        writer.WriteLine(final[i].Value[0]);
                   }
                }

                //            for (int y = 0; y < rt.Count; y++)

                //                for (int i=0;i<result.Count;i++)
                //    {
                //        var rt = result[i].Value.Distinct().ToList();
                //        writer.WriteLine(result[i].Key);
                //        if (rt.Count > 1 )
                //        {

                //            for(int y =0 ; y<rt.Count; y++)

                //            {
                //                if(y==rt.Count-1)
                //                {
                //                    writer.WriteLine(rt[y].Key);
                //                    for (int k = 0; k < rt[y].Value.Count; k++)
                //                    {
                //                        writer.WriteLine(rt[y].Value[k]);
                //                    }
                //                    break;
                //                }
                //                else if(rt[y].Key != rt[y+1].Key)
                //                writer.WriteLine(rt[y].Key);
                //                for( int k=0;k<rt[y].Value.Count;k++)
                //                {
                //                    writer.WriteLine(rt[y].Value[k]);
                //                }

                //            }
                //        }

                //        else
                //        {
                //            writer.WriteLine(result[i].Value[0]);
                //        }

                //    }

            }
        }


        public static RedTail810 DeSerialize(string path)
        {
            RedTail810 r = null;
            XDocument xdoc = XDocument.Load(path);
            xdoc.Declaration = null;
            XmlSerializer s = new XmlSerializer(typeof(RedTail810));
            r = (RedTail810)s.Deserialize(xdoc.CreateReader());

            return r;

        }

    
    }
}



        
    

