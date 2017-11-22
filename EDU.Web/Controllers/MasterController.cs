using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EZY.EDU.Contract;
using EZY.EDU.BusinessFactory;

using EDU.Web.ViewModels.Master;

namespace EDU.Web.Controllers
{
    [SessionFilter]
    public class MasterController : BaseController
    {
        //#region Product Category
        ///* Product Category Start */
        //[HttpGet]
        //public ActionResult ProductCategoryList()
        //{
        //    var productCategoryList = new ProductCategoryBO()
        //                .GetList()
        //                .ToList();
        //    return View(productCategoryList);
        //}

        //[HttpGet]
        //public PartialViewResult ProductCategoryGroup(short? productCategory)
        //{
        //    var lookupBO = new LookupBO();
        //    ViewBag.ProductCategoryList = lookupBO.GetList()
        //                               .Where(x => x.LookupCategory == "CategoryGroup")
        //                               .Select(x => new LookUpSelect
        //                               {
        //                                   Value = x.LookupID,
        //                                   Text = x.LookupDescription
        //                               }).ToList<LookUpSelect>();
        //    if (productCategory == null)
        //    {
        //        ViewBag.Title = "New Product Category";
        //        return PartialView("ProductCategory", new ProductCategoryMaster());
        //    }
        //    else
        //    {
        //        ViewBag.Title = "Update Product Category";
        //        return PartialView("ProductCategory", new ProductCategoryBO()
        //                                .GetList()
        //                                .Where(x=>x.ProductCategory== productCategory)
        //                                .FirstOrDefault());

        //    }
        //}
        //[HttpPost]
        //public ActionResult ProductCategory(ProductCategoryMaster productCategoryMaster)
        //{
        //    //productCategoryMaster.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_PRODUCTCATEGORY;
        //    var lookUpBO = new ProductCategoryBO();
        //    var result = lookUpBO.SaveProduct(productCategoryMaster);

        //    return RedirectToAction("ProductCategoryList");
        //}
        ///*  Product Category End */
        //#endregion

        #region Category Group
        /* Category Group Start */
        [HttpGet]
        public ActionResult CategoryList()
        {
            var CategoryList = new LookupBO()
                        .GetList()
                        .Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CATEGORY)
                        .ToList();

            return View(CategoryList);
        }

        public PartialViewResult CategoryGroup(short? lookupID)
        {
            if (lookupID == null)
            {
                ViewBag.Title = "New Category Group";
                return PartialView(new Lookup());
            }
            else
            {
                ViewBag.Title = "Update Category Group";
                return PartialView(new LookupBO()
                                        .GetList()
                                        .Where(x => x.LookupID == lookupID)
                                        .FirstOrDefault());

            }
        }
        [HttpPost]
        public ActionResult SaveCategoryGroup(Lookup lookUp)
        {
            lookUp.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_CATEGORY;
            lookUp.Status = true;
            lookUp.ISOCode = "";
            lookUp.MappingCode = "";
            lookUp.CreatedBy = USER_ID;
            lookUp.CreatedOn = UTILITY.SINGAPORETIME;
            lookUp.ModifiedBy = USER_ID;
            lookUp.ModifiedOn = UTILITY.SINGAPORETIME;

            var lookUpBO = new LookupBO();
            var result = lookUpBO.SaveLookup(lookUp);

            return RedirectToAction("CategoryList");
        }
        [HttpPost]
        public JsonResult SaveCategory(string ProductCategory)
        {
            Lookup lookUp = new Lookup();
            lookUp.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_CATEGORY;
            lookUp.Status = true;
            lookUp.ISOCode = "";
            lookUp.MappingCode = "";
            lookUp.CreatedBy = USER_ID;
            lookUp.CreatedOn = UTILITY.SINGAPORETIME;
            lookUp.ModifiedBy = USER_ID;
            lookUp.ModifiedOn = UTILITY.SINGAPORETIME;
            lookUp.LookupDescription = "Desc";
            lookUp.LookupCode = ProductCategory;
            var lookUpBO = new LookupBO();
            var result = lookUpBO.SaveLookup(lookUp);
            ViewBag.ProductCategoryList = lookUpBO.GetList()
                                    .Where(x => x.LookupCategory == "CategoryGroup")
                                    .Select(x => new LookUpSelect
                                    {
                                        Value = x.LookupID,
                                        Text = x.LookupCode
                                    }).ToList<LookUpSelect>();
            return Json(ViewBag.ProductCategoryList, JsonRequestBehavior.AllowGet);
        }
        /* Category Group End */
        #endregion

        //#region Currency
        ///* Currency Start */
        //[HttpGet]
        //public ActionResult CurrencyList()
        //{
        //    var currencyCategoryList = new LookupBO()
        //                .GetList()
        //                .Where(x => x.LookupCategory == UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY)
        //                .ToList();

        //    return View(currencyCategoryList);
        //}

        //public PartialViewResult Currency(short? lookupID)
        //{
        //    if (lookupID == null)
        //    {
        //        ViewBag.Title = "New Currency";
        //        return PartialView(new Lookup());
        //    }
        //    else
        //    {
        //        ViewBag.Title = "Update Currency";
        //        return PartialView(new LookupBO()
        //                                .GetList()
        //                                .Where(x => x.LookupID == lookupID)
        //                                .FirstOrDefault());

        //    }
        //}

        //[HttpPost]
        //public ActionResult Currency(Lookup lookUp)
        //{
        //    lookUp.LookupCategory = UTILITY.CONFIG_LOOKUPCATEGORY_CURRENCY;
        //    lookUp.Status = true;
        //    lookUp.ISOCode = "";
        //    lookUp.MappingCode = "";
        //    lookUp.CreatedBy = USER_ID;
        //    lookUp.CreatedOn = UTILITY.SINGAPORETIME;
        //    lookUp.ModifiedBy = USER_ID;
        //    lookUp.ModifiedOn = UTILITY.SINGAPORETIME;

        //    var lookUpBO = new LookupBO();
        //    var result = lookUpBO.SaveLookup(lookUp);

        //    return RedirectToAction("CurrencyList");
        //}
        ///* Currency End */
        //#endregion

        //#region Product
        //[HttpGet]
        //public ActionResult Products()
        //{
        //    return View("Products", new List<Product>());
        //}

        //public PartialViewResult Product(string productCode = "", string modelNo = "")
        //{
        //    var lookupBO = new LookupBO();
        //    ViewBag.ProductCategoryList = lookupBO.GetList()
        //                            .Where(x => x.LookupCategory == "CategoryGroup")
        //                            .Select(x => new LookUpSelect
        //                            {
        //                                Value = x.LookupID,
        //                                Text = x.LookupCode
        //                            }).ToList<LookUpSelect>();
        //    if (string.IsNullOrWhiteSpace(productCode))
        //    {

        //        ViewBag.Title = "New Product";
        //        return PartialView(new Product { Status = true });
        //    }
        //    else
        //    {
        //        ViewBag.Title = "Update Product";
        //        return PartialView(new ProductBO().GetProduct(new Product { ProductCode = productCode, ModelNo = modelNo }));

        //    }
        //}

        //[HttpPost]
        //public ActionResult Product(Product product)
        //{
        //    var productBO = new ProductBO();

        //    product.ProductCode = product.ModelNo;
        //    product.CreatedBy = USER_ID;
        //    product.CreatedOn = UTILITY.SINGAPORETIME;
        //    product.ModifiedBy = USER_ID;
        //    product.ModifiedOn = UTILITY.SINGAPORETIME;
        //    var result = productBO.SaveProduct(product);

        //    return RedirectToAction("Products");
        //}

        //[HttpPost]
        //public ActionResult DeleteProduct(string productCode, string modelNo)
        //{
        //    var productBO = new ProductBO();
        //    var result = productBO.DeleteProduct(new Product { ProductCode = productCode, ModelNo = modelNo });
        //    return View("Products", productBO.GetList());
        //}

        //[HttpPost]
        //public JsonResult GetProducts(DataTableObject Obj)
        //    {
        //    var products = new ProductBO().GetProductTableDataList(Obj);
        //    var totalRecords = new ProductBO().GetList().Count();

        //    return Json(new { products = products, totalRecords = totalRecords }, JsonRequestBehavior.AllowGet);
        //}

        //#endregion

        #region EduProduct
        [HttpGet]
        public ViewResult EduProductList()
        {
            var list = new EduProductBO().GetList();
            return View("EduProductList", list.AsEnumerable());
        }

        [HttpGet]
        public PartialViewResult EduProduct(int? Id)
        {
            if (!Id.HasValue)
            {
                ViewBag.Title = "New Product";
                return PartialView(new EduProduct { Id = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Product";
                return PartialView(new EduProductBO().GetEduProduct(new EduProduct { Id = Id.Value }));

            }
        }

        [HttpPost]
        public RedirectToRouteResult SaveEduProduct(EduProduct eduProduct)
        {
            eduProduct.ProductDescription = eduProduct.ProductName;

            eduProduct.CreatedBy = USER_ID;
            eduProduct.CreatedOn = UTILITY.SINGAPORETIME;
            eduProduct.ModifiedBy = USER_ID;
            eduProduct.ModifiedOn = UTILITY.SINGAPORETIME;
            if (!IsEduProductExists(eduProduct.ProductName))
            {
                var result = new EduProductBO().SaveEduProduct(eduProduct);
            }

            return RedirectToAction("EduProductList");
        }

        [HttpPost]
        public ActionResult DeleteEduProduct(int? Id)
        {
            var result = new EduProductBO().DeleteEduProduct(new EduProduct { Id = Id.Value });

            var list = new EduProductBO().GetList();
            return View("EduProductList", list.AsEnumerable());
        }


        [HttpGet]
        public bool IsEduProductExists(string productName)
        {
            return new EduProductBO().IsEduProductExists(new EduProduct { ProductName = productName });
        }

        #endregion

        #region Course
        [HttpGet]
        public ViewResult CourseList()
        {
            var list = new CourseBO().GetList().AsEnumerable();
            return View("CourseList", list);
        }

        [HttpGet]
        public PartialViewResult Course(int? Id)
        {
            ViewData["ProductData"] = new EduProductBO().GetList();
            ViewData["CountryData"] = new BranchBO().GetList();
            if (!Id.HasValue)
            {
                ViewBag.Title = "New Course";
                return PartialView(new Course { Id = -1, IsActive = true });
            }
            else
            {
                ViewBag.Title = "Update Course";
                return PartialView(new CourseBO().GetCourse(new Course { Id = Id.Value }));
            }
        }

        [HttpGet]
        public JsonResult GetCourse(int Id)
        {
            return Json(new CourseBO().GetCourse(new Course { Id = Id }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public RedirectToRouteResult SaveCourse(Course course)
        {
            course.CourseDescription = course.CourseName;
            course.CreatedBy = USER_ID;
            course.CreatedOn = UTILITY.SINGAPORETIME;
            course.ModifiedBy = USER_ID;
            course.ModifiedOn = UTILITY.SINGAPORETIME;
            course.AvailableSeats = 0;
            var result = new CourseBO().SaveCouse(course);

            return RedirectToAction("CourseList");
        }


        [HttpPost]
        public ActionResult DeleteEduCourse(int? Id)
        {
            var result = new CourseBO().DeleteEduCourse(new Course { Id = Id.Value });

            var list = new CourseBO().GetList();
            return View("CourseList", list);
        }

        [HttpGet]
        public bool IsEduCourseExists(string courseName, string country, int product)
        {
            return new CourseBO().IsEduCourseExists(new Course { CourseName = courseName, Country = country, Product = product, });
        }
        #endregion

        #region Course Sales Master
        [HttpGet]
        public ViewResult CourseSalesMasterList()
        {
            var list = new CourseSalesMasterBO().GetList();
            var totalleadsonhand = 0;
            long totalrevenue = 0;

            foreach (var item in list)
            {
                totalleadsonhand += item.LeadsOnHead;
                totalrevenue += Convert.ToInt64(item.Revenue == null ? 0 : item.Revenue.Value);
            }

            ViewData["totalleadsonhand"] = totalleadsonhand;
            ViewData["totalrevenue"] = totalrevenue;

            return View("CourseSalesMasterList", list.AsEnumerable());
        }

        [HttpGet]
        public ViewResult CourseSalesMaster(int Id)
        {
            CourseSalesMaster courseSalesMaster = null;
            var courseSalesMasterVm = new CourseSalesMasterVm
            {
                eduProductList = new EduProductBO().GetList().AsEnumerable(),
                branchList = new BranchBO().GetList().AsEnumerable(),
                monthList = GetMonthData(),
                courseSalesMaster = courseSalesMaster
            };

            if (Id == -1)
            {
                courseSalesMaster = new CourseSalesMaster
                {
                    Id = Id
                    //StartDate = UTILITY.SINGAPORETIME,
                    //EndDate = UTILITY.SINGAPORETIME,
                    //RegClosingDate = UTILITY.SINGAPORETIME.AddDays(-14)
                };
            }
            else
            {
                courseSalesMaster = new CourseSalesMasterBO()
                                            .GetCourseSalesMaster(new CourseSalesMaster { Id = Id });

                courseSalesMasterVm.courseList = new CourseBO()
                                                    .GetCoursesByProduct(courseSalesMaster.Product)
                                                    .AsEnumerable();
            }

            courseSalesMasterVm.courseSalesMaster = courseSalesMaster;
            return View("CourseSalesMaster", courseSalesMasterVm);
        }

        [HttpPost]
        public RedirectToRouteResult CoureSalesMaster(CourseSalesMaster courseSalesMaster)
        {
            courseSalesMaster.IsActive = true;
            courseSalesMaster.CreatedBy = USER_ID;
            courseSalesMaster.CreatedOn = UTILITY.SINGAPORETIME;
            courseSalesMaster.ModifiedBy = USER_ID;
            courseSalesMaster.ModifiedOn = UTILITY.SINGAPORETIME;
            courseSalesMaster.ConfirmOrDropDate = UTILITY.SINGAPORETIME;

            courseSalesMaster.Id = new CourseSalesMasterBO().SaveCourseSalesMaster(courseSalesMaster);
            return RedirectToAction("CourseSalesMasterList");

            //var courseSalesMasterVm = new CourseSalesMasterVm
            //{
            //    eduProductList = new EduProductBO().GetList().AsEnumerable(),
            //    branchList = new BranchBO().GetList().AsEnumerable(),
            //    monthList = GetMonthData(),
            //    courseSalesMaster = courseSalesMaster
            //};

            //courseSalesMaster = new CourseSalesMasterBO()
            //                                .GetCourseSalesMaster(new CourseSalesMaster { Id = courseSalesMaster.Id });

            //courseSalesMasterVm.courseList = new CourseBO()
            //                                    .GetCoursesByProduct(courseSalesMaster.Product)
            //                                    .AsEnumerable();


            //courseSalesMasterVm.courseSalesMaster = courseSalesMaster;
            //return View("CourseSalesMaster", courseSalesMasterVm);
        }


        [HttpPost]
        public ActionResult DeleteCourseSalesMaster(int? Id)
        {
            var result = new CourseSalesMasterBO().DeleteCourseSalesMaster(new CourseSalesMaster { Id = Id.Value });

            return View("CourseSalesMasterList",
               new CourseSalesMasterBO().GetList().AsEnumerable());
        }

        //[HttpPost]
        //public ActionResult ConfirmCoureSalesMaster(int Id, Int16 Registered, string RegisteredRemarks)
        //{
        //    var result = new CourseSalesMasterBO().ConfirmCoureSalesMaster(new CourseSalesMaster { Id = Id, Registered = Registered, RegisteredRemarks = RegisteredRemarks });

        //    return View("CourseSalesMasterList",
        //       new CourseSalesMasterBO().GetList().AsEnumerable());
        //}
        #endregion
    }
}