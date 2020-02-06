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

		public int Compare(object x, object y)
		{
			Address a1 = (Address)x;
			Address a2 = (Address)y;

			
			if (a1.EntityIDCode == a2.EntityIDCode &&
				a1.AccountingIDCode == a2.AccountingIDCode &&
				a1.Name == a2.Name &&
				a1.Address1 == a2.Address1 && 
				a1.Address2 == a2.Address2 && 
				a1.Address3 == a2.Address3 && 
				a1.City == a2.City &&
				a1.State == a2.State &&
				a1.Zip == a2.Zip &&
				a1.Country == a2.Country
			)
				return 0;
			else
				return -1;

			throw new NotImplementedException();
		}
	}

	[XmlRoot(ElementName = "AddressGroup")]
	public class AddressGroup : IComparer
	{
		[XmlElement(ElementName = "Address")]
		public List<Address> Address { get; set; }

		public int Compare(object x, object y)
		{
			bool result = true;

			AddressGroup ag1 = (AddressGroup)x;
			AddressGroup ag2 = (AddressGroup)y;
			ag1.Address.OrderBy(z => z.AccountingIDCode);
			ag2.Address.OrderBy(z => z.AccountingIDCode);
			if (ag1 != null && ag2 != null && ag1.Address != null && ag2.Address != null && ag1.Address.Count == ag2.Address.Count )
			{
				for(int i = 0; i<ag1.Address.Count; i++)
				{
					if(ag1.Address[i].EntityIDCode != "SF" && ag2.Address[i].EntityIDCode != "SF")
					result &= ag1.Address[i].Compare(ag1.Address[i], ag2.Address[i]) == 0;
				}
			}
			else if ((ag1 == null && ag2 == null) || (ag1.Address == null && ag2.Address == null))
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

		public int Compare(object x, object y)
		{
			LotRecord lr1 = (LotRecord)x;
			LotRecord lr2 = (LotRecord)y;
			
			if (lr1.LotNumber == lr2.LotNumber && lr1.LotQty == lr2.LotQty && lr1.LotExpiration == lr2.LotExpiration)
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
		public List<LotRecord> LotRecord { get; set; }

		public int Compare(object x, object y)
		{
			bool result1 = true;

			LotGroup lg1 = (LotGroup)x;
			LotGroup lg2 = (LotGroup)y;

			if ((lg1 == null && lg2 != null) || (lg2 == null && lg1 != null))

			{
				result1 = false;
			}
			else if(lg1.LotRecord!= null && lg2.LotRecord != null)
			{
				lg1.LotRecord = lg1.LotRecord.OrderBy(z => z.LotNumber).ToList();
				lg2.LotRecord = lg2.LotRecord.OrderBy(z=> z.LotNumber).ToList();
					for (int i = 0; i < lg1.LotRecord.Count; i++)
					{
					
						result1 = lg1.LotRecord[i].Compare(lg1.LotRecord[i], lg2.LotRecord[i]) == 0;
					}
				
			}
			else if ((lg1 == null && lg2 == null) || (lg1.LotRecord == null && lg2.LotRecord == null))
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

		public int Compare(object x1, object y1)
		{

			InvoiceItemGroup ig1 = (InvoiceItemGroup)x1;
			InvoiceItemGroup ig2 = (InvoiceItemGroup)y1;
			
			if (ig1.AccountingItemNumber == ig2.AccountingItemNumber && 
				ig1.AccountingDescription == ig2.AccountingDescription &&
				ig1.InvoiceLineNumber == ig2.InvoiceLineNumber &&
				ig1.RedTailPOLineID == ig2.RedTailPOLineID &&
				ig1.Warehouse == ig2.Warehouse &&
				ig1.QtyOrdered == ig2.QtyOrdered &&
				ig1.QtyShipped == ig2.QtyShipped &&
				ig1.QtyBackOrd == ig2.QtyBackOrd &&
				ig1.DiscountPercent == ig2.DiscountPercent &&
				ig1.UOM == ig2.UOM &&
				ig1.UnitPrice == ig2.UnitPrice &&
				ig1.NetUnitPrice == ig2.NetUnitPrice &&
				ig1.ExtendedNetPrice == ig2.ExtendedNetPrice
				// Tax group not needed to be equal
			//ig1.LotGroup.Compare(ig1.LotGroup, ig2.LotGroup) == 0 
				//ig1.CommentGroup.Compare(ig1.CommentGroup, ig2.CommentGroup) == 0
				) 
				
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
		public List<InvoiceItemGroup> InvoiceItemGroup { get; set; }
		[XmlElement(ElementName = "CommentGroup")]
		public CommentGroup CommentGroup { get; set; }

		public int Compare(object x, object y)
		{
			bool result = true;

			InvoiceHeaderGroup invHdr1 = (InvoiceHeaderGroup)x;
			InvoiceHeaderGroup invHdr2 = (InvoiceHeaderGroup)y;

			if(invHdr1 != null && invHdr2 != null)
			{
				if (
					invHdr1.InvoiceNumber == invHdr2.InvoiceNumber &&
					invHdr1.InvoiceType == invHdr2.InvoiceType &&
					invHdr1.InvoiceResend == invHdr2.InvoiceResend &&
					invHdr1.OrderShipmentStatus == invHdr2.OrderShipmentStatus &&
					invHdr1.InvoiceDate == invHdr2.InvoiceDate &&
					invHdr1.SalesOrderNumber == invHdr2.SalesOrderNumber &&
					invHdr1.SalesOrderDate == invHdr2.SalesOrderDate &&
					invHdr1.PurchaseOrderNumber == invHdr2.PurchaseOrderNumber &&
					invHdr1.RedTailPOID == invHdr2.RedTailPOID &&
					invHdr1.CurrencyCode == invHdr2.CurrencyCode &&
					invHdr1.InvoiceAmount == invHdr2.InvoiceAmount &&
					invHdr1.TaxAmount == invHdr2.TaxAmount &&
					invHdr1.FreightAmount == invHdr2.FreightAmount &&
					invHdr1.MiscChargeAmount == invHdr2.MiscChargeAmount &&
					invHdr1.SalesDiscAmount == invHdr2.SalesDiscAmount &&
					invHdr1.TermsDescription == invHdr2.TermsDescription &&
					invHdr1.TermsDiscountPercent == invHdr2.TermsDiscountPercent &&
					invHdr1.TermsDiscountDays == invHdr2.TermsDiscountDays &&
					invHdr1.TermsNetDueDays == invHdr2.TermsNetDueDays &&
					invHdr1.TermsDiscountDate == invHdr2.TermsDiscountDate &&
					invHdr1.TermsNetDueDate == invHdr2.TermsNetDueDate &&
					invHdr1.TermsDiscountBasis == invHdr2.TermsDiscountBasis &&
					invHdr1.TermsDiscountAmount == invHdr2.TermsDiscountAmount &&
					invHdr1.NetInvoiceAmount == invHdr2.NetInvoiceAmount &&
					invHdr1.ShipVia == invHdr2.ShipVia &&
					invHdr1.AddressGroup.Compare(invHdr1.AddressGroup, invHdr2.AddressGroup) == 0 
					//invHdr1.CommentGroup.Compare(invHdr1.CommentGroup, invHdr2.CommentGroup) == 0
				)
				{
					if (invHdr1.InvoiceItemGroup != null && invHdr2.InvoiceItemGroup != null && invHdr1.InvoiceItemGroup.Count == invHdr2.InvoiceItemGroup.Count)
					{
						for(int i=0; i<invHdr1.InvoiceItemGroup.Count; i++)
						{
							result &= invHdr1.InvoiceItemGroup[i].Compare(invHdr1.InvoiceItemGroup[i], invHdr2.InvoiceItemGroup[i]) == 0;
						}
					}
				}
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

		public int Compare(object x, object y)
		{
			InvoiceHeaderGroup inv1 = (InvoiceHeaderGroup)x;
			InvoiceHeaderGroup inv2 = (InvoiceHeaderGroup)y;
			
			if(inv1 != null && inv2 != null)
			{
				if (inv1.Compare(inv1, inv2) == 0)
					return 0;
			}
			return 1;
		}
	}

	[XmlRoot(ElementName = "CommentGroup")]
	public class CommentGroup : IComparer
	{
		[XmlElement(ElementName = "Comment")]
		public List<string> Comment { get; set; }

		public int Compare(object x, object y)
		{
			bool result = true;

			CommentGroup cg1 = (CommentGroup)x;
			CommentGroup cg2 = (CommentGroup)y;

			if(cg1 != null && cg2 != null && cg1.Comment != null && cg2.Comment != null && cg1.Comment.Count == cg2.Comment.Count)
			{
				for(int i=0; i<cg1.Comment.Count; i++)
				{
					result = result & cg1.Comment[i] == cg2.Comment[i];
				}
			}
			else if ((cg1 == null && cg2 == null) || (cg1.Comment == null && cg2.Comment == null))
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
