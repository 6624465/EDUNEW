using EDU.Web.Models;
using EDU.Web.ViewModels.VendorPaymentStatusModel;
using EZY.EDU.BusinessFactory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class VendorPaymentStatusController : BaseController
    {
        // GET: VendorPaymentStatus
        EducationEntities dbContext = new EducationEntities();
        public ActionResult VendorPaymentStatusList(short? month, int year)
        {
            try
            {
                VendorPaymentStatusVM result = new VendorPaymentStatusVM();
                List<TrainingConfirmation> trainingConfirmationList = dbContext.TrainingConfirmations.Where(x => x.IsActive == true && x.Year == year && x.Month == month).ToList();
                List<string> list = trainingConfirmationList.Select(x => x.TrainingConfirmationID).ToList();

                List<Registration> registrations = dbContext.Registrations.Where(x => x.IsActive == true && list.Contains(x.TrainingConfirmationID) && x.BalanceAmount > 0).ToList();
                var courseList = new CourseBO().GetList().Where(x => x.IsActive == true).ToList();
                var productList = new EduProductBO().GetList().Where(x => x.IsActive == true).ToList();

                List<VendorPaymentVM> VendorPaymentVM = dbContext.VendorPayments
                     .Where(x => x.IsActive == true && list.Contains(x.TrainingConfirmationID) && x.BalanceAmount > 0)
                    .Select(x => new VendorPaymentVM
                    {
                        VendorPaymentId = x.VendorPaymentId,
                        VendorId = x.VendorId,
                        TrainingConfirmationID = x.TrainingConfirmationID,
                        InvoiceAmount = x.InvoiceAmount,
                        PaidAmount = x.PaidAmount,
                        BalanceAmount = x.BalanceAmount,
                        OtherReceivablesAmount = x.OtherReceivablesAmount,
                        TotalAmount = x.TotalAmount,
                        DueDate = x.DueDate,
                        ReceiptDate = x.ReceiptDate,
                        ReferenceDoc = x.ReferenceDoc,
                        IsActive = true,
                        CreatedBy = x.CreatedBy,
                        CreatedOn = x.CreatedOn,
                        ModifiedBy = x.ModifiedBy,
                        ModifiedOn = x.ModifiedOn,
                        Year = year,
                        Month = month
                    })
                    .ToList();


                foreach (var item in registrations)
                {
                    if (VendorPaymentVM.Where(x => x.TrainingConfirmationID == item.TrainingConfirmationID).Count() == 0)
                    {
                        decimal? invoiceAmount = 0;
                        decimal? paidAmount = 0;
                        decimal? balanceAmount = 0;

                        foreach (var ritem in registrations.Where(t => t.TrainingConfirmationID == item.TrainingConfirmationID))
                        {
                            invoiceAmount += ritem.TotalAmount;
                            paidAmount += (ritem.Payment1 == null ? 0 : ritem.Payment1) + (ritem.Payment2 == null ? 0 : ritem.Payment2) + (ritem.Payment3 == null ? 0 : ritem.Payment3);
                            balanceAmount += ritem.BalanceAmount;
                        }
                        var vendorId = trainingConfirmationList.Where(x => x.TrainingConfirmationID == item.TrainingConfirmationID).FirstOrDefault().TrianerId;
                        VendorPaymentVM.Add(new VendorPaymentVM()
                        {
                            VendorPaymentId = -1,
                            VendorId = vendorId,
                            TrainingConfirmationID = item.TrainingConfirmationID,
                            InvoiceAmount = invoiceAmount,
                            PaidAmount = paidAmount,
                            BalanceAmount = balanceAmount,
                            OtherReceivablesAmount = 0,
                            TotalAmount = 0,
                            DueDate = null,
                            ReceiptDate = null,
                            ReferenceDoc = null,
                            IsActive = true,
                            CreatedBy = null,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = null,
                            ModifiedOn = null,
                            Year = year,
                            Month = month
                        });
                    }
                }

                result.VendorPayment = VendorPaymentVM.OrderBy(x => x.TrainingConfirmationID).ToList();
                result.trainingconfList = trainingConfirmationList;
                result.productList = new EduProductBO().GetList();
                result.courseList = new CourseBO().GetList();
                result.trainerInformationList = dbContext.TrainerInformations.Where(x => x.IsActive == true).ToList();

                long Invoiceamount = 0;
                long Paidamount = 0;
                long Balanceamount = 0;
                long otherreceivablesamount = 0;
                long totalamount = 0;
                foreach (var item in VendorPaymentVM)
                {
                    Invoiceamount += Convert.ToInt64(item.InvoiceAmount == null ? 0 : item.InvoiceAmount.Value);
                    Paidamount += Convert.ToInt64(item.PaidAmount == null ? 0 : item.PaidAmount.Value);
                    Balanceamount += Convert.ToInt64(item.BalanceAmount == null ? 0 : item.BalanceAmount.Value);
                    otherreceivablesamount += Convert.ToInt64(item.OtherReceivablesAmount == null ? 0 : item.OtherReceivablesAmount.Value);
                    totalamount += Convert.ToInt64(item.TotalAmount == null ? 0 : item.TotalAmount.Value);
                }

                List<decimal?> summary = new List<decimal?>();
                summary.Add(Invoiceamount);
                summary.Add(Paidamount);
                summary.Add(Balanceamount);
                summary.Add(otherreceivablesamount);
                summary.Add(totalamount);

                ViewData["Summary"] = summary;
                return View(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private VendorPaymentStatusVM getListData(string trainingConfirmationID)
        //{
        //    VendorPaymentStatusVM result = new VendorPaymentStatusVM();
        //    try
        //    {
        //        //List<TrainingConfirmation> tc = dbContext.TrainingConfirmations.Where(x => x.IsActive == true).ToList();
        //        //List<Registration> regList = GetList(trainingConfirmationID);
        //        //TrainingConfirmation tcdtl = tc.Where(x => x.TrainingConfirmationID == trainingConfirmationID).FirstOrDefault();


        //        //TrainingConfirmDtl tcd = new TrainingConfirmDtl();
        //        //string VendorName = "";
        //        //if (tcdtl != null)
        //        //{
        //        //    string productName = new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault() != null ? new EduProductBO().GetList().Where(x => x.Id == tcdtl.Product).FirstOrDefault().ProductName : "";
        //        //    string courseName = new CourseBO().GetList().Where(x => x.Id == tcdtl.Course && x.Product == tcdtl.Product && x.Country == tcdtl.Country).FirstOrDefault() != null ? new CourseBO().GetList().Where(x => x.Id == tcdtl.Course && x.Product == tcdtl.Product && x.Country == tcdtl.Country).FirstOrDefault().CourseName : "";
        //        //    VendorName = dbContext.TrainerInformations.Where(t => t.TrianerId == tcdtl.TrianerId).FirstOrDefault() == null ? "" : dbContext.TrainerInformations.Where(t => t.TrianerId == tcdtl.TrianerId).FirstOrDefault().VendorName;

        //        //    tcd = new TrainingConfirmDtl()
        //        //    {
        //        //        Id = tcdtl.Id,
        //        //        TrainingConfirmationID = tcdtl.TrainingConfirmationID,
        //        //        Product = tcdtl.Product,
        //        //        Course = tcdtl.Course,
        //        //        ProductName = productName,
        //        //        CourseName = courseName,
        //        //        TrianerId = tcdtl.TrianerId
        //        //    };
        //        //}

        //        List<VendorPaymentVM> VendorPaymentVM = dbContext.VendorPayments
        //            .Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID)
        //            .Select(x => new VendorPaymentVM
        //            {
        //                VendorPaymentId = x.VendorPaymentId,
        //                VendorId = x.VendorId,
        //                TrainingConfirmationID = x.TrainingConfirmationID,
        //                InvoiceAmount = x.InvoiceAmount,
        //                PaidAmount = x.PaidAmount,
        //                BalanceAmount = x.BalanceAmount,
        //                OtherReceivablesAmount = x.OtherReceivablesAmount,
        //                TotalAmount = x.TotalAmount,
        //                DueDate = x.DueDate,
        //                ReceiptDate = x.ReceiptDate,
        //                ReferenceDoc = x.ReferenceDoc,
        //                IsActive = true,
        //                CreatedBy = x.CreatedBy,
        //                CreatedOn = x.CreatedOn,
        //                ModifiedBy = x.ModifiedBy,
        //                ModifiedOn = x.ModifiedOn,
        //                //VendorName = VendorName
        //            })
        //            .ToList();

        //        decimal? invoiceAmount = 0;
        //        decimal? paidAmount = 0;
        //        decimal? balanceAmount = 0;

        //        //foreach (var item in regList)
        //        //{
        //        //    invoiceAmount += item.TotalAmount;
        //        //    paidAmount += (item.Payment1 == null ? 0 : item.Payment1) + (item.Payment2 == null ? 0 : item.Payment2) + (item.Payment3 == null ? 0 : item.Payment3);
        //        //    balanceAmount += item.BalanceAmount;
        //        //}


        //        if (VendorPaymentVM.Count() == 0 && trainingConfirmationID != "")
        //        {
        //            VendorPaymentVM.Add(new VendorPaymentVM()
        //            {
        //                VendorPaymentId = -1,
        //                //VendorId = tcd.TrianerId,
        //                //TrainingConfirmationID = tcd.TrainingConfirmationID,
        //                InvoiceAmount = invoiceAmount,
        //                PaidAmount = paidAmount,
        //                BalanceAmount = balanceAmount,
        //                OtherReceivablesAmount = 0,
        //                TotalAmount = 0,
        //                DueDate = null,
        //                ReceiptDate = null,
        //                ReferenceDoc = null,
        //                IsActive = true,
        //                CreatedBy = null,
        //                CreatedOn = DateTime.Now,
        //                ModifiedBy = null,
        //                ModifiedOn = null,
        //                //VendorName = VendorName
        //            });
        //        }

        //        //result.VendorPayment = VendorPaymentVM;

        //        //result.trainingconf = tc;
        //        //result.trainingconfDetail = tcd;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        [HttpPost]
        public PartialViewResult VendorPaymentStatusDetail(VendorPaymentVM VendorPayment)
        {
            ViewData["year"] = VendorPayment.Year;
            ViewData["month"] = VendorPayment.Month;

            List<string> list = dbContext.TrainerInformations.Where(x => x.IsActive == true).Select(x => x.VendorName).Distinct().ToList();

            List<TrainerInformation> vendorsList = new List<TrainerInformation>();

            foreach (var item in list)
            {
                var trainerid = dbContext.TrainerInformations.Where(x => x.VendorName == item).FirstOrDefault().TrianerId;
                vendorsList.Add(new TrainerInformation()
                {
                    TrianerId = trainerid,
                    VendorName = item
                });
            }
            ViewData["VendorsList"] = vendorsList;

            VendorPayment.VendorName = VendorPayment.VendorId == 0 ? "" : dbContext.TrainerInformations.Where(x => x.TrianerId == VendorPayment.VendorId).FirstOrDefault().VendorName;
            VendorPayment.VendorId = VendorPayment.VendorId == 0 ? 0 : dbContext.TrainerInformations.Where(x => x.VendorName == VendorPayment.VendorName).FirstOrDefault().TrianerId;

            if (VendorPayment.VendorPaymentId == -1)
            {
                ViewBag.Title = "New Vendor Payment Status";
                VendorPayment.IsActive = true;
            }
            else
            {
                ViewBag.Title = "Update Vendor Payment Status";
            }
            return PartialView(VendorPayment);
        }

        //private List<Registration> GetList(string trainingConfirmationID)
        //{
        //    List<Registration> List = dbContext.Registrations
        //        .Where(x => x.IsActive == true && x.TrainingConfirmationID == trainingConfirmationID)
        //        .ToList();
        //    return List;
        //}

        [HttpPost]
        public ActionResult SaveVendorPaymentStatus(VendorPaymentVM Vendorpaymentvm)
        {
            try
            {
                VendorPayment Vendorpayment = new VendorPayment();
                if (Vendorpaymentvm.VendorPaymentId == -1)
                {
                    Vendorpayment.VendorPaymentId = Vendorpaymentvm.VendorPaymentId;
                    Vendorpayment.VendorId = Vendorpaymentvm.VendorId;
                    Vendorpayment.TrainingConfirmationID = Vendorpaymentvm.TrainingConfirmationID;
                    Vendorpayment.InvoiceAmount = Vendorpaymentvm.InvoiceAmount;
                    Vendorpayment.PaidAmount = Vendorpaymentvm.PaidAmount;
                    Vendorpayment.BalanceAmount = Vendorpaymentvm.BalanceAmount;
                    Vendorpayment.OtherReceivablesAmount = Vendorpaymentvm.OtherReceivablesAmount;
                    Vendorpayment.TotalAmount = Vendorpaymentvm.TotalAmount;
                    Vendorpayment.DueDate = Vendorpaymentvm.DueDate;

                    Vendorpayment.ReceiptDate = Vendorpaymentvm.ReceiptDate;
                    Vendorpayment.IsActive = true;
                    Vendorpayment.CreatedBy = USER_ID;
                    Vendorpayment.CreatedOn = UTILITY.SINGAPORETIME;
                    if (Vendorpaymentvm.FileName != null && Vendorpaymentvm.FileName.ContentLength > 0)
                    {
                        Vendorpayment.ReferenceDoc = Vendorpaymentvm.FileName.FileName;
                    }
                    dbContext.VendorPayments.Add(Vendorpayment);

                    dbContext.SaveChanges();

                    if (Vendorpaymentvm.FileName != null && Vendorpaymentvm.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + "VPS_" + Vendorpayment.VendorPaymentId + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        Vendorpaymentvm.FileName.SaveAs(path + Vendorpaymentvm.FileName.FileName);
                    }
                }
                else
                {
                    Vendorpayment = dbContext.VendorPayments.Where(x => x.VendorPaymentId == Vendorpaymentvm.VendorPaymentId).FirstOrDefault();

                    Vendorpayment.VendorId = Vendorpaymentvm.VendorId;
                    Vendorpayment.TrainingConfirmationID = Vendorpaymentvm.TrainingConfirmationID;
                    Vendorpayment.InvoiceAmount = Vendorpaymentvm.InvoiceAmount;
                    Vendorpayment.PaidAmount = Vendorpaymentvm.PaidAmount;
                    Vendorpayment.BalanceAmount = Vendorpaymentvm.BalanceAmount;
                    Vendorpayment.OtherReceivablesAmount = Vendorpaymentvm.OtherReceivablesAmount;
                    Vendorpayment.TotalAmount = Vendorpaymentvm.TotalAmount;
                    Vendorpayment.DueDate = Vendorpaymentvm.DueDate;

                    Vendorpayment.ReceiptDate = Vendorpaymentvm.ReceiptDate;
                    Vendorpayment.IsActive = true;
                    Vendorpayment.ModifiedBy = USER_ID;
                    Vendorpayment.ModifiedOn = UTILITY.SINGAPORETIME;
                    if (Vendorpaymentvm.FileName != null && Vendorpaymentvm.FileName.ContentLength > 0)
                    {
                        Vendorpayment.ReferenceDoc = Vendorpaymentvm.FileName.FileName;
                    }
                    dbContext.SaveChanges();

                    if (Vendorpaymentvm.FileName != null && Vendorpaymentvm.FileName.ContentLength > 0)
                    {
                        string path = Server.MapPath("~/Uploads/" + "VPS_" + Vendorpayment.VendorPaymentId + "/");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        Vendorpaymentvm.FileName.SaveAs(path + Vendorpaymentvm.FileName.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction("VendorPaymentStatusList", new { month = Vendorpaymentvm.Month, year = Vendorpaymentvm.Year });
        }

        [HttpPost]
        public JsonResult DeleteVendorPaymentStatus(int VendorPaymentId)
        {
            VendorPayment deleteVendor = dbContext.VendorPayments.Where(x => x.VendorPaymentId == VendorPaymentId).FirstOrDefault();
            if (deleteVendor != null)
            {
                deleteVendor.IsActive = false;
                dbContext.SaveChanges();
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}