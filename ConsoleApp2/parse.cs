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
     
        public static List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
        private static int sum = 0;

        static void Main(string[] args)
        {
            Console.WriteLine("Enter Target Path");
            string targetPath = Console.ReadLine();
            Console.WriteLine("Enter Source Path");
            string sourcePath = Console.ReadLine();
            Console.WriteLine("Enter Result File Path");
            string resultFilePath = Console.ReadLine();

            //string targetPath = @"C:\Users\schaturvedi\Downloads\Invoices (1)";
            //string sourcePath = @"C:\Users\schaturvedi\Downloads\810_20200131_032553";
            //string resultFilePath = @"C:\Users\schaturvedi\Desktop\result.txt\";

            string[] sourceFile = Directory.GetFiles(sourcePath, "*.xml");

            var parent = DeSerialize(sourceFile[0]);
            var parentInvoices = parent?.InvoiceGroup?.ToDictionary(x => x.InvoiceHeaderGroup.InvoiceNumber, x => x.InvoiceHeaderGroup);

            Dictionary<string, InvoiceHeaderGroup> childInvoices = new Dictionary<string, InvoiceHeaderGroup>();
            string[] targetFiles = Directory.GetFiles(targetPath, "*.xml");
            foreach (var t in targetFiles)
            {
                var childInvoice = DeSerialize(t);
                if(!childInvoices.ContainsKey(childInvoice.InvoiceGroup[0].InvoiceHeaderGroup.InvoiceNumber))
                {
                    childInvoices.Add(childInvoice.InvoiceGroup[0].InvoiceHeaderGroup.InvoiceNumber, childInvoice.InvoiceGroup[0].InvoiceHeaderGroup);

                }
                
            }
            List<KeyValuePair<string, string>> res = new List<KeyValuePair<string, string>>();


            //Code=2 when Invoice is available in meta but not in target
            List<string> availableInMasterAbsentInTarget = new List<string>();
            availableInMasterAbsentInTarget = parentInvoices.Keys.Except(childInvoices.Keys).ToList();
           // res.AddRange(availableInMasterAbsentInTarget.Select(x => new KeyValuePair<string, string>(x, "Invoice is available in source but not in target")).ToList());
            availableInMasterAbsentInTarget.ForEach(x => parentInvoices.Remove(x));
            //Code=3 when Invoice is available in target but not in meta
            List<string> availableInTargetAbsentInMaster = new List<string>();
            availableInTargetAbsentInMaster = childInvoices.Keys.Except(parentInvoices.Keys).ToList();
           // res.AddRange(availableInTargetAbsentInMaster.Select(x => new KeyValuePair<string, string>(x, "Invoice is available in target but not in source")).ToList());
            availableInTargetAbsentInMaster.ForEach(x => childInvoices.Remove(x));

            foreach (KeyValuePair<string, InvoiceHeaderGroup> entry in childInvoices)
            {
                string key = entry.Key;
                InvoiceHeaderGroup value = entry.Value;

                InvoiceHeaderGroup par = parentInvoices[key];


                AddressGroup obj = new AddressGroup();
                InvoiceItemGroup IIG = new InvoiceItemGroup();
                LotGroup lt = new LotGroup();
                LotRecord lr = new LotRecord();
                InvoiceGroup ig = new InvoiceGroup();

                 /*if (ig.Compare(par, value) == 0)
                 {
                     Console.WriteLine("True");
                 }
                 else
                 {
                     Console.WriteLine("false");
                 }*/


                //      
        //        for (int i = 0; i < par.AddressGroup.Address.Count; i++)
      //          {

                    //if (par.AddressGroup.Address[i].EntityIDCode != "SF")
                    //{
                        int ret = obj.Compare(value.AddressGroup, par.AddressGroup);
                        if (ret == -1)
                        {
                            result.Insert(0, new KeyValuePair<string, string>(key, "Adddress"));
                        }
                    //}
                //}



                ///if (par.InvoiceItemGroup.Count == value.InvoiceItemGroup.Count)
                //{
                for (int i = 0; i < par.InvoiceItemGroup.Count; i++)
                {
                    int ret1 = IIG.Compare(par.InvoiceItemGroup[i], value.InvoiceItemGroup[i]);
                    if (ret1 == -1)
                    {
                        result.Insert(0, new KeyValuePair<string, string>(key, "InvoiceItemGroup"));
                    }
                }



                for (int i = 0; i < par.InvoiceItemGroup.Count; i++)
                {
                    int ret2 = lt.Compare(par.InvoiceItemGroup[i].LotGroup, value.InvoiceItemGroup[i].LotGroup);
                    sum = sum + ret2;
                }

                if (sum > 0)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "LotRecord"));
                }



                if (par.CommentGroup != value.CommentGroup)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "CommentGroup"));
                }

                if (par.CurrencyCode != value.CurrencyCode)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "CurrencyCode"));
                }
                if (par.FreightAmount != value.FreightAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "FreightAmount"));
                }
                if (par.InvoiceAmount != value.InvoiceAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "InvoiceAmount"));
                }
                if (par.InvoiceDate != value.InvoiceDate)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "InvoiceDate"));
                }
                if (par.InvoiceNumber != value.InvoiceNumber)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "InvoiceNumber"));
                }
                if (par.InvoiceResend != value.InvoiceResend)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "InvoiceResend"));
                }
                if (par.InvoiceType != value.InvoiceType)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "InvoiceType"));
                }
                if (par.MiscChargeAmount != value.MiscChargeAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "MiscChargeAmount"));
                }
                if (par.NetInvoiceAmount != value.NetInvoiceAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "NetInvoiceAmount"));
                }
                if (par.OrderShipmentStatus != value.OrderShipmentStatus)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "OrderShipmentStatus"));
                }
                if (par.PurchaseOrderNumber != value.PurchaseOrderNumber)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "PurchaseOrderNumber"));
                }
                if (par.RedTailPOID != value.RedTailPOID)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "RedTailPOID"));
                }
                if (par.SalesDiscAmount != value.SalesDiscAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "SalesDiscAmount"));
                }
                if (par.SalesOrderDate != value.SalesOrderDate)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "SalesOrderDate"));
                }
                if (par.SalesOrderNumber != value.SalesOrderNumber)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "SalesOrderNumber"));
                }
                if (par.ShipVia != value.ShipVia)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "ShipVia"));
                }
                if (par.TaxAmount != value.TaxAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TaxAmount"));
                }
                if (par.TermsDiscountAmount != value.TermsDiscountAmount)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsDiscountAmount"));
                }
                if (par.TermsDiscountBasis != value.TermsDiscountBasis)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsDiscountBasis"));
                }
                if (par.TermsDiscountDate != value.TermsDiscountDate)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsDiscountDate"));
                }
                if (par.TermsDiscountDays != value.TermsDiscountDays)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsDiscountDays"));
                }
                if (par.TermsDiscountPercent != value.TermsDiscountPercent)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsDiscountPercent"));
                }
                if (par.TermsNetDueDate != value.TermsNetDueDate)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsNetDueDate"));
                }
                if (par.TermsNetDueDays != value.TermsNetDueDays)
                {
                    result.Insert(0, new KeyValuePair<string, string>(key, "TermsNetDueDays"));
                }
                sum = 0;
            }
            using (StreamWriter writer = new StreamWriter(resultFilePath))

            {
                foreach (var x in result)
                {
                    writer.WriteLine(x);
                }

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



        
    

