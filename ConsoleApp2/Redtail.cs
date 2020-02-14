using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp2
{
	[XmlRoot(ElementName = "Sender")]
	public class Sender
	{ 
		[XmlElement(ElementName = "SenderName")]
		public string SenderName { get; set; }
		[XmlElement(ElementName = "RedTailClientID")]
		public string RedTailClientID { get; set; }
		[XmlElement(ElementName = "Source")]
		public string Source { get; set; }
		[XmlElement(ElementName = "Filename")]
		public string Filename { get; set; }
		[XmlElement(ElementName = "DateCreated")]
		public string DateCreated { get; set; }
	}

	[XmlRoot(ElementName = "Address")]
	public class Address : IComparer
	{
		[XmlElement(ElementName = "EntityIDCode")]
		public string EntityIDCode { get; set; }
		[XmlElement(ElementName = "AccountingIDCode")]
		public string AccountingIDCode { get; set; }
		[XmlElement(ElementName = "Name")]
		public string Name { get; set; }
		[XmlElement(ElementName = "Address1")]
		public string Address1 { get; set; }
		[XmlElement(ElementName = "Address2")]
		public string Address2 { get; set; }
		[XmlElement(ElementName = "Address3")]
		public string Address3 { get; set; }
		[XmlElement(ElementName = "City")]
		public string City { get; set; }
		[XmlElement(ElementName = "State")]
		public string State { get; set; }
		[XmlElement(ElementName = "Zip")]
		public string Zip { get; set; }
		[XmlElement(ElementName = "Country")]
		public string Country { get; set; }

		public static List<string> addr = new List<string>();
		public int Compare(object x, object y)
		{
			Address a1 = (Address)x;
			Address a2 = (Address)y;

			if (a1.EntityIDCode != a2.EntityIDCode)
			{
				parse.elementList.Insert(0, "EntityIDCode");
				addr.Insert(0, "EntityIDCode");
			}
			if (a1.AccountingIDCode != a2.AccountingIDCode)
			{
				parse.elementList.Insert(0, "AccountingIDCode");
				addr.Insert(0, "AccountingIDCode");
			}
			if (a1.Name != a2.Name)
			{
				parse.elementList.Insert(0 ,  "Name");
				addr.Insert(0, "Name");
			}
					if(a1.Address1 != a2.Address1)
			{
				parse.elementList.Insert(0, "Address1");
				addr.Insert(0, "Address1");
			}
					if(a1.Address2 != a2.Address2)
			{
				parse.elementList.Insert(0, "Address2");
				addr.Insert(0, "Address2");

			}
			if (a1.Address3 != a2.Address3)
			{
				parse.elementList.Insert(0, "Address3");
				addr.Insert(0, "Address3");

			}
			if (a1.City != a2.City)
			{
				parse.elementList.Insert(0, "City");
				addr.Insert(0, "City");

			}
			if (a1.State != a2.State)
			{
				parse.elementList.Insert(0, "State");
				addr.Insert(0, "State");

			}
			if (a1.Zip != a2.Zip)
			{
				parse.elementList.Insert(0, "Zip");
				addr.Insert(0, "Zip");

			}
			if (a1.Country != a2.Country)
			{
				parse.elementList.Insert(0, "Country");
				addr.Insert(0, "Country");

			}

			
			if (addr.Count == 0)
		
			{
				return 0;
			}
			else
				return -1;

			throw new NotImplementedException();
		}
	}

	[XmlRoot(ElementName = "AddressGroup")]
	public class AddressGroup : IComparer
	{
		[XmlElement(ElementName = "Address")]
		public List<Address> Addresses { get; set; }

		public int Compare(object x, object y)
		{
			bool result = true;
			List<KeyValuePair<string,List<string>>> adg = new List<KeyValuePair<string, List<string>>>();
			AddressGroup ag1 = (AddressGroup)x;
			AddressGroup ag2 = (AddressGroup)y;
			ag1.Addresses.OrderBy(z => z.AccountingIDCode);
			ag2.Addresses.OrderBy(z => z.AccountingIDCode);
			if (ag1 != null && ag2 != null && ag1.Addresses != null && ag2.Addresses != null && ag1.Addresses.Count == ag2.Addresses.Count )
			{
				for(int i = 0; i<ag1.Addresses.Count; i++)
				{
					if(!parse.line.Contains(ag1.Addresses[i].EntityIDCode) && !parse.line.Contains(ag2.Addresses[i].EntityIDCode))
					//if (ag1.Addresses[i].EntityIDCode != "SF" && ag2.Addresses[i].EntityIDCode != "SF")
						if (ag1.Addresses[i].Compare(ag1.Addresses[i], ag2.Addresses[i]) != 0)
						{
							result = false;
							//parse.elementList.Insert(0, "Address");
						}
				}
				if(Address.addr.Count > 0)
				adg.Insert(0, new KeyValuePair<string, List<string>>("AddressGroup", Address.addr));
			}
			else if ((ag1 == null && ag2 == null) || (ag1.Addresses == null && ag2.Addresses == null))
			{
				result = true;
			}
			else
			{
				result = false;
			}

			return result ? 0 : 1;
		}
	}

	[XmlRoot(ElementName = "LotRecord")]
	public class LotRecord : IComparer
	{
		[XmlElement(ElementName = "LotNumber")]
		public string LotNumber { get; set; }
		[XmlElement(ElementName = "LotQty")]
		public string LotQty { get; set; }
		[XmlElement(ElementName = "LotExpiration")]
		public string LotExpiration { get; set; }

		public static List<string> lotr = new List<string>();
		public int Compare(object x, object y)
		{
			LotRecord lr1 = (LotRecord)x;
			LotRecord lr2 = (LotRecord)y;

			if (lr1.LotNumber != lr2.LotNumber)
			{
				parse.elementList.Insert(0, "LotNumber");
				lotr.Insert(0, "LotNumber");
			}

			if (lr1.LotQty != lr2.LotQty)
			{
				parse.elementList.Insert(0, "LotQty");
				lotr.Insert(0, "LotQty");

			}
			if (lr1.LotExpiration != lr2.LotExpiration)
			{
				parse.elementList.Insert(0, "LotExpiration");
				lotr.Insert(0, "LotExpiration");

			}
			if (lotr.Count == 0)
			return 0;
			else
				return -1;

			throw new NotImplementedException();
		}
	}

	[XmlRoot(ElementName = "LotGroup")]
	public class LotGroup : IComparer 
	{
		[XmlElement(ElementName = "LotRecord")]
		public List<LotRecord> Lotrecordee { get; set; }
		public static List<KeyValuePair<string , List<string>>> ltg = new List<KeyValuePair<string, List<string>>>();
		public int Compare(object x, object y)
		{
			bool result1 = true;

			LotGroup lg1 = (LotGroup)x;
			LotGroup lg2 = (LotGroup)y;

			if ((lg1 == null && lg2 != null) || (lg2 == null && lg1 != null))

			{
				result1 = false;
			}
			else if(lg1.Lotrecordee!= null && lg2.Lotrecordee != null)
			{
				lg1.Lotrecordee = lg1.Lotrecordee.OrderBy(z => z.LotNumber).ToList();
				lg2.Lotrecordee = lg2.Lotrecordee.OrderBy(z=> z.LotNumber).ToList();
				for (int i = 0; i < lg1.Lotrecordee.Count; i++)
				{

					if (lg1.Lotrecordee[i].Compare(lg1.Lotrecordee[i], lg2.Lotrecordee[i]) != 0)
					{
						result1 = false;
						//parse.elementList.Insert(0, "LotRecord");
					}

				}
				if(LotRecord.lotr.Count > 0)
				ltg.Insert(0, new KeyValuePair<string, List<string>>("LotGroup", LotRecord.lotr));

			}
			else if ((lg1 == null && lg2 == null) || (lg1.Lotrecordee == null && lg2.Lotrecordee == null))
			{
				result1 = true;
			}
			else
			{
				result1 = false;
				//parse.result.Insert(0, new KeyValuePair<string, string>(, "LotRecord"));
			}

			return result1 ? 0 : 1;
		}
	}

	[XmlRoot(ElementName = "InvoiceItemGroup")]
	public class InvoiceItemGroup : IComparer
	{
		[XmlElement(ElementName = "AccountingItemNumber")]
		public string AccountingItemNumber { get; set; }
		[XmlElement(ElementName = "AccountingDescription")]
		public string AccountingDescription { get; set; }
		[XmlElement(ElementName = "InvoiceLineNumber")]
		public string InvoiceLineNumber { get; set; }
		[XmlElement(ElementName = "RedTailPOLineID")]
		public string RedTailPOLineID { get; set; }
		[XmlElement(ElementName = "Warehouse")]
		public string Warehouse { get; set; }
		[XmlElement(ElementName = "QtyOrdered")]
		public string QtyOrdered { get; set; }
		[XmlElement(ElementName = "QtyShipped")]
		public string QtyShipped { get; set; }
		[XmlElement(ElementName = "QtyBackOrd")]
		public string QtyBackOrd { get; set; }
		[XmlElement(ElementName = "DiscountPercent")]
		public string DiscountPercent { get; set; }
		[XmlElement(ElementName = "UOM")]
		public string UOM { get; set; }
		[XmlElement(ElementName = "UnitPrice")]
		public string UnitPrice { get; set; }
		[XmlElement(ElementName = "NetUnitPrice")]
		public string NetUnitPrice { get; set; }
		[XmlElement(ElementName = "ExtendedNetPrice")]
		public string ExtendedNetPrice { get; set; }
		[XmlElement(ElementName = "TaxGroup")]
		public string TaxGroup { get; set; }
		[XmlElement(ElementName = "LotGroup")]
		public LotGroup LotGroup { get; set; }
		[XmlElement(ElementName = "CommentGroup")]
		public CommentGroup CommentGroup { get; set; }
		public static List<string> InvItmGrp = new List<string>();
		public static List<KeyValuePair<string, List<string>>> I = new List<KeyValuePair<string, List<string>>>();
		public int Compare(object x1, object y1)
		{

			InvoiceItemGroup ig1 = (InvoiceItemGroup)x1;
			InvoiceItemGroup ig2 = (InvoiceItemGroup)y1;
			
			if (ig1.AccountingItemNumber != ig2.AccountingItemNumber)
			{
				parse.elementList.Insert(0, "AccountingItemNumber");
				InvItmGrp.Insert(0, "AccountingItemNumber");
			}
			if (ig1.AccountingDescription != ig2.AccountingDescription)
			{
				parse.elementList.Insert(0, "AccountingDescription");
				InvItmGrp.Insert(0, "AccountingDescription");


			}
			if (ig1.InvoiceLineNumber != ig2.InvoiceLineNumber)
			{
				parse.elementList.Insert(0, "InvoiceLineNumber");
				InvItmGrp.Insert(0, "InvoiceLineNumber");

			}
			if (ig1.RedTailPOLineID != ig2.RedTailPOLineID)
			{
				parse.elementList.Insert(0, "RedTailPOLineID");
				InvItmGrp.Insert(0, "RedTailPOLineID");

			}
			if (ig1.Warehouse != ig2.Warehouse)
				{
				parse.elementList.Insert(0, "Warehouse");
				InvItmGrp.Insert(0, "Warehouse");

			}
			if (ig1.QtyOrdered != ig2.QtyOrdered)
			{
				parse.elementList.Insert(0, "QtyOrdered");
				InvItmGrp.Insert(0, "QtyOrdered");

			}
			if (ig1.QtyShipped != ig2.QtyShipped)
			{
				parse.elementList.Insert(0, "QtyShipped");
				InvItmGrp.Insert(0, "QtyShipped");

			}
			if (ig1.QtyBackOrd != ig2.QtyBackOrd)
			{
				parse.elementList.Insert(0, "QtyBackOrd");
				InvItmGrp.Insert(0, "QtyBackOrd");

			}
			if (ig1.DiscountPercent != ig2.DiscountPercent)
			{
				parse.elementList.Insert(0, "DiscountPercent");
				InvItmGrp.Insert(0, "DiscountPercent");

			}
			if (ig1.UOM != ig2.UOM)
			{
				parse.elementList.Insert(0, "UOM");
				InvItmGrp.Insert(0, "UOM");

			}
			if (ig1.UnitPrice != ig2.UnitPrice)
			{
				parse.elementList.Insert(0, "UnitPrice");
				InvItmGrp.Insert(0, "UnitPrice");

			}
			if (ig1.NetUnitPrice != ig2.NetUnitPrice)
			{
				parse.elementList.Insert(0, "NetUnitPrice");
				InvItmGrp.Insert(0, "NetUnitPrice");

			}
			if (ig1.ExtendedNetPrice != ig2.ExtendedNetPrice)
			{
				parse.elementList.Insert(0, "ExtendedNetPrice");
				InvItmGrp.Insert(0, "ExtendedNetPrice");

			}
			//bool t1, t2;
			//if (ig1.CommentGroup != null)
			//{
			//	string obj = ig1.CommentGroup.ToString();
			//	string obj2 = obj.Remove(0, 12);
			//	 t1 = !parse.line.Contains(obj2);
			//}
			//if (ig2.CommentGroup != null)
			//{
			//	string objx = ig2.CommentGroup.ToString();
			//	string obj1 = objx.Remove(0, 12);
			//	 t2 = !parse.line.Contains(obj1);
			//}
			//	if(!(t1&t2) = 0)
				
			
			
			//	if ((ig1.CommentGroup != null && ig2.CommentGroup == null)
			//		|| (ig1.CommentGroup != null && ig2.CommentGroup == null))
			//{
			//	parse.elementList.Insert(0, "CommentGroup");

			//}
			//else if (ig1.CommentGroup == null && ig2.CommentGroup == null)
			//{
			//}
			//else
			//{

			//	ig1.CommentGroup.Compare(ig1.CommentGroup, ig1.CommentGroup);

			//}
			if (!parse.line.Contains(ig1.TaxGroup) && !parse.line.Contains(ig2.TaxGroup))
			{
				if(ig1.TaxGroup != ig2.TaxGroup)
				{
					InvItmGrp.Insert(0, "TaxGroup");
				}
			}
			if(InvItmGrp.Count > 0)
				I.Insert(0, new KeyValuePair<string, List<string>>("InvoiceItemGroup", InvoiceItemGroup.InvItmGrp));
			// Tax group not needed to be equal
			if (ig1.LotGroup != null && ig1.LotGroup != null)
			{
				if (ig1.LotGroup.Compare(ig1.LotGroup, ig2.LotGroup) != 0)
				{
					I.Insert(0, new KeyValuePair<string, List<string>>("LotGroup", LotRecord.lotr));
					//parse.elementList.Insert(0, "LotGroup");
					return -1;
					
				}
			}
			else if((ig1.LotGroup != null && ig2.LotGroup == null) || (ig2.LotGroup != null && ig1.LotGroup == null) )
			{
				parse.elementList.Insert(0, "LotGroup");

			}

			if (InvItmGrp.Count == 0 || LotRecord.lotr.Count == 0)
				return 0;
			else
				return -1;
			throw new NotImplementedException();
		}
	}

	[XmlRoot(ElementName = "InvoiceHeaderGroup")]
	public class InvoiceHeaderGroup : IComparer
	{
		

		[XmlElement(ElementName = "InvoiceNumber")]
		public string InvoiceNumber { get; set; }
		[XmlElement(ElementName = "InvoiceType")]
		public string InvoiceType { get; set; }
		[XmlElement(ElementName = "InvoiceResend")]
		public string InvoiceResend { get; set; }
		[XmlElement(ElementName = "OrderShipmentStatus")]
		public string OrderShipmentStatus { get; set; }
		[XmlElement(ElementName = "InvoiceDate")]
		public string InvoiceDate { get; set; }
		[XmlElement(ElementName = "SalesOrderNumber")]
		public string SalesOrderNumber { get; set; }
		[XmlElement(ElementName = "SalesOrderDate")]
		public string SalesOrderDate { get; set; }
		[XmlElement(ElementName = "PurchaseOrderNumber")]
		public string PurchaseOrderNumber { get; set; }
		[XmlElement(ElementName = "RedTailPOID")]
		public string RedTailPOID { get; set; }
		[XmlElement(ElementName = "CurrencyCode")]
		public string CurrencyCode { get; set; }
		[XmlElement(ElementName = "InvoiceAmount")]
		public string InvoiceAmount { get; set; }
		[XmlElement(ElementName = "TaxAmount")]
		public string TaxAmount { get; set; }
		[XmlElement(ElementName = "FreightAmount")]
		public string FreightAmount { get; set; }
		[XmlElement(ElementName = "MiscChargeAmount")]
		public string MiscChargeAmount { get; set; }
		[XmlElement(ElementName = "SalesDiscAmount")]
		public string SalesDiscAmount { get; set; }
		[XmlElement(ElementName = "TermsDescription")]
		public string TermsDescription { get; set; }
		[XmlElement(ElementName = "TermsDiscountPercent")]
		public string TermsDiscountPercent { get; set; }
		[XmlElement(ElementName = "TermsDiscountDays")]
		public string TermsDiscountDays { get; set; }
		[XmlElement(ElementName = "TermsNetDueDays")]
		public string TermsNetDueDays { get; set; }
		[XmlElement(ElementName = "TermsDiscountDate")]
		public string TermsDiscountDate { get; set; }
		[XmlElement(ElementName = "TermsNetDueDate")]
		public string TermsNetDueDate { get; set; }
		[XmlElement(ElementName = "TermsDiscountBasis")]
		public string TermsDiscountBasis { get; set; }
		[XmlElement(ElementName = "TermsDiscountAmount")]
		public string TermsDiscountAmount { get; set; }
		[XmlElement(ElementName = "NetInvoiceAmount")]
		public string NetInvoiceAmount { get; set; }
		[XmlElement(ElementName = "ShipVia")]
		public string ShipVia { get; set; }
		[XmlElement(ElementName = "AddressGroup")]
		public AddressGroup AddressGroup { get; set; }
		[XmlElement(ElementName = "InvoiceItemGroup")]
		public List<InvoiceItemGroup> InvoiceItemGrouprr { get; set; }
		[XmlElement(ElementName = "CommentGroup")]
		public CommentGroup CommentGroup { get; set; }
		
		public static List<string> Invhdrgrp = new List<string>();
		public static List<KeyValuePair<string, List<string>>> ihg = new List<KeyValuePair<string, List<string>>>();
		public int Compare(object x, object y)
		{
			bool result = true;

			InvoiceHeaderGroup invHdr1 = (InvoiceHeaderGroup)x;
			InvoiceHeaderGroup invHdr2 = (InvoiceHeaderGroup)y;

			if (invHdr1 != null && invHdr2 != null)
			{
				if (invHdr1.InvoiceNumber != invHdr2.InvoiceNumber)
				{
					parse.elementList.Insert(0, "InvoiceNumber");
					Invhdrgrp.Insert(0, "InvoiceNumber");

				}

				if (invHdr1.InvoiceType != invHdr2.InvoiceType)
				{
					parse.elementList.Insert(0, "InvoiceType");
					Invhdrgrp.Insert(0, "InvoiceType");
				}

				if (invHdr1.InvoiceResend != invHdr2.InvoiceResend)
				{
					Invhdrgrp.Insert(0, "InvoiceResend");

					parse.elementList.Insert(0, "InvoiceResend");
				}

				if (invHdr1.OrderShipmentStatus != invHdr2.OrderShipmentStatus)
				{
					parse.elementList.Insert(0, "OrderShipmentStatus");
					Invhdrgrp.Insert(0, "OrderShipmentStatus");

				}

				if (invHdr1.InvoiceDate != invHdr2.InvoiceDate)
				{
					parse.elementList.Insert(0, "InvoiceDate");
					Invhdrgrp.Insert(0, "InvoiceDate");

				}
				if (invHdr1.SalesOrderNumber != invHdr2.SalesOrderNumber)
				{
					parse.elementList.Insert(0, "SalesOrderNumber");
					Invhdrgrp.Insert(0, "SalesOrderNumber");

				}
				if (invHdr1.SalesOrderDate != invHdr2.SalesOrderDate)
				{
					parse.elementList.Insert(0, "SalesOrderDate");
					Invhdrgrp.Insert(0, "SalesOrderDate");

				}
				if (invHdr1.PurchaseOrderNumber != invHdr2.PurchaseOrderNumber)
				{
					parse.elementList.Insert(0, "PurchaseOrderNumber");
					Invhdrgrp.Insert(0, "PurchaseOrderNumber");

				}
				if (invHdr1.RedTailPOID != invHdr2.RedTailPOID)
				{
					parse.elementList.Insert(0, "RedTailPOID");
					Invhdrgrp.Insert(0, "RedTailPOID");

				}
				if (invHdr1.CurrencyCode != invHdr2.CurrencyCode)
				{
					parse.elementList.Insert(0, "CurrencyCode");
					Invhdrgrp.Insert(0, "CurrencyCode");

				}
				if (invHdr1.InvoiceAmount != invHdr2.InvoiceAmount)
				{
					parse.elementList.Insert(0, "InvoiceAmount");
					Invhdrgrp.Insert(0, "InvoiceAmount");

				}
				if (invHdr1.TaxAmount != invHdr2.TaxAmount)
				{
					parse.elementList.Insert(0, "TaxAmount");
					Invhdrgrp.Insert(0, "TaxAmount");

				}
				if (invHdr1.FreightAmount != invHdr2.FreightAmount)
				{
					parse.elementList.Insert(0, "FreightAmount");
					Invhdrgrp.Insert(0, "FreightAmount");

				}
				if (invHdr1.MiscChargeAmount != invHdr2.MiscChargeAmount)
				{
					parse.elementList.Insert(0, "MiscChargeAmount");
					Invhdrgrp.Insert(0, "MiscChargeAmount");

				}
				if (invHdr1.SalesDiscAmount != invHdr2.SalesDiscAmount)
				{
					parse.elementList.Insert(0, "SalesDiscAmount");
					Invhdrgrp.Insert(0, "SalesDiscAmount");
				}
				if (invHdr1.TermsDescription != invHdr2.TermsDescription)
				{
					parse.elementList.Insert(0, "TermsDescription");
					Invhdrgrp.Insert(0, "TermsDescription");

				}
				if (invHdr1.TermsDiscountPercent != invHdr2.TermsDiscountPercent)
				{
					parse.elementList.Insert(0, "TermsDiscountPercent");
					Invhdrgrp.Insert(0, "TermsDiscountPercent");

				}
				if (invHdr1.TermsDiscountDays != invHdr2.TermsDiscountDays)
				{
					parse.elementList.Insert(0, "TermsDiscountDays");
					Invhdrgrp.Insert(0, "TermsDiscountDays");

				}
				if (invHdr1.TermsNetDueDays != invHdr2.TermsNetDueDays)
				{
					parse.elementList.Insert(0, "TermsNetDueDays");
					Invhdrgrp.Insert(0, "TermsNetDueDays");

				}
				if (invHdr1.TermsDiscountDate != invHdr2.TermsDiscountDate)
				{
					parse.elementList.Insert(0, "TermsDiscountDate");
					Invhdrgrp.Insert(0, "TermsDiscountDate");

				}
				if (invHdr1.TermsNetDueDate != invHdr2.TermsNetDueDate)
				{
					parse.elementList.Insert(0, "TermsNetDueDate");
					Invhdrgrp.Insert(0, "TermsNetDueDate");

				}
				if (invHdr1.TermsDiscountBasis != invHdr2.TermsDiscountBasis)
				{
					parse.elementList.Insert(0, "TermsDiscountBasis");
					Invhdrgrp.Insert(0, "TermsDiscountBasis");

				}
				if (invHdr1.TermsDiscountAmount != invHdr2.TermsDiscountAmount)
				{
					parse.elementList.Insert(0, "TermsDiscountAmount");
					Invhdrgrp.Insert(0, "TermsDiscountAmount");

				}
				if (invHdr1.NetInvoiceAmount != invHdr2.NetInvoiceAmount)
				{
					parse.elementList.Insert(0, "NetInvoiceAmount");
					Invhdrgrp.Insert(0, "NetInvoiceAmount");

				}
				if (invHdr1.ShipVia != invHdr2.ShipVia)
				{
					parse.elementList.Insert(0, "ShipVia");
					Invhdrgrp.Insert(0, "ShipVia");

				}
				//if(!parse.line.Contains(invHdr1.AddressGroup.Addresses[0].EntityIDCode) &&
				//	!parse.line.Contains(invHdr2.AddressGroup.Addresses[0].EntityIDCode))
					//if (invHdr1.AddressGroup.Addresses[0].EntityIDCode != "SF" &&
					//invHdr2.AddressGroup.Addresses[0].EntityIDCode != "SF")
				{
					if (invHdr1.AddressGroup.Compare(invHdr1.AddressGroup, invHdr2.AddressGroup) != 0)
					{
						//ihg.Insert(0, new KeyValuePair<string, List<string>>("AddressGroup", Address.addr));
						//ihg.Insert
						result = false;
						//parse.elementList.Insert(0, addr);
				}
				} //if(!parse.line.Contains(CommentGroup) &&  )
				//string obj = invHdr2.InvoiceItemGrouprr[0].CommentGroup.ToString();
				//string obj2 = obj.Remove(0, 12);
				//if (invHdr1.CommentGroup != null)
				//{
				//	string objx = invHdr1.InvoiceItemGrouprr[0].CommentGroup.ToString();
				//}
				//string obj1 = objx.Remove(0, 12);
				//if (!parse.line.Contains(obj1) && !parse.line.Contains(obj2))
					if ((invHdr1.CommentGroup != null && invHdr2.CommentGroup == null)
						|| (invHdr2.CommentGroup != null && invHdr1.CommentGroup == null))
					{
						parse.elementList.Insert(0, "CommentGroup");

					}
					else if (invHdr1.CommentGroup == null && invHdr2.CommentGroup == null)
					{
					}
					else
					{

						invHdr1.CommentGroup.Compare(invHdr1.CommentGroup, invHdr2.CommentGroup);

					}
				if (invHdr1.InvoiceItemGrouprr != null && invHdr2.InvoiceItemGrouprr != null && invHdr1.InvoiceItemGrouprr.Count == invHdr2.InvoiceItemGrouprr.Count)
					{
					
					for (int i = 0; i < invHdr1.InvoiceItemGrouprr.Count; i++)
					{
						if (invHdr1.InvoiceItemGrouprr[i].Compare(invHdr1.InvoiceItemGrouprr[i], invHdr2.InvoiceItemGrouprr[i]) != 0)
						{
					
							//if(LotRecord.lotr.Count > 0)

							result = false;
						//parse.elementList.Insert(0,ihg.Insert(0, new KeyValuePair<string, List<string>>("InvoiceItemGroup", InvoiceHeaderGroup.Invhdrgrp)); "InvoiceItemGroup");
					}
					}
					}

				if (CommentGroup != null)
				{
					if (CommentGroup.cmnt.Count > 0)
						ihg.Insert(0, new KeyValuePair<string, List<string>>("<CommentGroup>", CommentGroup.cmnt));
				}
					
				if (LotRecord.lotr.Count > 0)
					ihg.Insert(0, new KeyValuePair<string, List<string>>("<LotGroup>", LotRecord.lotr));
				if (InvoiceItemGroup.InvItmGrp.Count > 0)
					ihg.Insert(0, new KeyValuePair<string, List<string>>("<InvoiceItemGroup>", InvoiceItemGroup.InvItmGrp));
				if (Address.addr.Count > 0)
					ihg.Insert(0, new KeyValuePair<string, List<string>>("<AddressGroup>", Address.addr));
				if (Invhdrgrp.Count > 0)
			ihg.Insert(0, new KeyValuePair<string, List<string>>("<InvoiceHeaderGroup>", InvoiceHeaderGroup.Invhdrgrp));

			}
			else if(invHdr1 == null && invHdr2 == null)
			{
				result = true;
			}
			else
			{
				result = false;
			}

			return result ? 0 : 1 ;
		}
	}

	[XmlRoot(ElementName = "InvoiceGroup")]
	public class InvoiceGroup : IComparer
	{
		[XmlElement(ElementName = "InvoiceHeaderGroup")]
		public InvoiceHeaderGroup InvoiceHeaderGroup { get; set; }

		List<KeyValuePair<string, List<string>>> Invg = new List<KeyValuePair<string, List<string>>>();
		public int Compare(object x, object y)
		{
			InvoiceHeaderGroup inv1 = (InvoiceHeaderGroup)x;
			InvoiceHeaderGroup inv2 = (InvoiceHeaderGroup)y;
			
			if(inv1 != null && inv2 != null)
			{
				if (inv1.Compare(inv1, inv2) != 0)
				{
					//parse.elementList.Insert(0, "InvoiceHeaderGroup");
					//Invg.Insert(0 , new KeyValuePair<string, List<string>>("InvoiceGroup", ));
					Invg.Insert(0, new KeyValuePair<string, List<string>>("InvoiceGroup", InvoiceHeaderGroup.Invhdrgrp));
					return -1;
				}
				else
					return 0;
			}
			return 1;
		}
	}

	[XmlRoot(ElementName = "CommentGroup")]
	public class CommentGroup : IComparer
	{
		[XmlElement(ElementName = "Comment")]
		public List<string> Commentee { get; set; }

		public List<string> cmnt = new List<string>();
		public int Compare(object x, object y)
		{
			bool result = true;

			CommentGroup cg1 = (CommentGroup)x;
			CommentGroup cg2 = (CommentGroup)y;

			if(cg1 != null && cg2 != null && cg1.Commentee != null && cg2.Commentee != null && cg1.Commentee.Count == cg2.Commentee.Count)
			{
				for(int i=0; i<cg1.Commentee.Count; i++)
				{
					if(!parse.line.Contains(cg1.Commentee[i]) && !parse.line.Contains(cg2.Commentee[i]))
					result = result & cg1.Commentee[i] == cg2.Commentee[i];
					if ((cg1.Commentee[i] != cg2.Commentee[i]))
						cmnt.Insert(0, Commentee[i]);
				}
			}
			else if ((cg1 == null && cg2 == null) || (cg1.Commentee == null && cg2.Commentee == null))
			{
				result = true;
			}
			else
			{
				result = false;
			}

			return result ? 0 : 1;
		}
	}

	[XmlRoot(ElementName = "RedTail810")]
	public class RedTail810
	{
		[XmlElement(ElementName = "NumberOfDocuments")]
		public string NumberOfDocuments { get; set; }
		[XmlElement(ElementName = "Sender")]
		public Sender Sender { get; set; }
		[XmlElement(ElementName = "InvoiceGroup")]
		public List<InvoiceGroup> InvoiceGroup { get; set; }
	}

}
