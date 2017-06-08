﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web.UI.WebControls;
using Hotcakes.Commerce.Catalog;
using Hotcakes.Commerce.Content;
using Hotcakes.Commerce.Dnn.Utils;
using Hotcakes.Commerce.Tests.IRepository;
using Hotcakes.Commerce.Tests.XmlRepository;
using Hotcakes.Commerce.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Hotcakes.Commerce.Tests
{
    [TestClass]
    public class ProductImageRepositoryTest: BaseProductTest
    {
        /// <summary>
        /// The _irepocategory
        /// </summary>
        private IXmlProductImageRepository _irepoproductimg;

        /// <summary>
        /// Initializes.
        /// </summary>
        public ProductImageRepositoryTest()
        {
            _irepoproductimg = new XmlProductImageRepository();
        }

        /// <summary>
        /// Tests the in order.
        /// </summary>
        [TestMethod]
		public void ProductImage_TestInOrder()
        {
            CreateProduct();
           
            AddProductImage();
            LoadProductImage();
            SortProductImage();
            MergeProductImage();
            DeleteProductImage();
            DeleteProductAllImage();

        }

       #region Product Image Load/Add/Edit/Delete/Sort Test Cases
        /// <summary>
        /// Loads the product image.
        /// </summary>
        //[TestMethod]
        public void LoadProductImage()
        {
            //Arrange
            var count = _irepoproductimg.GetTotalProductImageCount();
            var prj = GetRootProduct();

            //Act
            var resultcount = _application.CatalogServices.ProductImages.FindByProductId(prj.Bvin);

            //Assert
            Assert.AreEqual(count, resultcount.Count);
        }

        /// <summary>
        /// Adds the product image.
        /// </summary>
        //[TestMethod]
        public void AddProductImage()
        {
            //Arrange
            var prjimages = _irepoproductimg.GetAddProductImage();
            var prj = GetRootProduct();
            var count = _application.CatalogServices.ProductImages.FindByProductId(prj.Bvin);
            var c = 0;


            //Act
            foreach (var productImage in prjimages)
            {
                c++;
                productImage.ProductId = prj.Bvin;
                productImage.Bvin = Guid.NewGuid().ToString();
                _application.CatalogServices.ProductImages.Create(productImage);
            }
            var resulycount = _application.CatalogServices.ProductImages.FindByProductId(prj.Bvin);

            //Assert
            Assert.AreEqual(count.Count + c, resulycount.Count);

        }

        /// <summary>
        /// Deletes the product image.
        /// </summary>
        //[TestMethod]
        public void DeleteProductImage()
        {
            var prj = GetRootProduct();
            var count = _application.CatalogServices.ProductImages.FindByProductId(prj.Bvin);

            if (count.Count == 0) Assert.Fail();

            //Act
            var imgbvin = count.OrderBy(x => x.LastUpdatedUtc).FirstOrDefault().Bvin;
            var img = _application.CatalogServices.ProductImages.Find(imgbvin);

            _application.CatalogServices.ProductImages.Delete(img.Bvin);
            //TODO:Need to change function DeleteAdditionalProductImage for set default portalid
            var resultcount = _application.CatalogServices.ProductImages.FindByProductId(prj.Bvin);

            //Assert
            Assert.AreEqual(count.Count - 1, resultcount.Count);
        }

        /// <summary>
        /// Sorts the product image.
        /// </summary>
        //[TestMethod]
        public void SortProductImage()
        {
            //Arrange
            var prj = GetRootProduct();
            var bvinarray = prj.Images.Select(x => x.Bvin).ToList();
            bvinarray.Reverse();

            //Act/Assert
            Assert.IsTrue(_application.CatalogServices.ProductImages.Resort(prj.Bvin, bvinarray));
        }

        /// <summary>
        /// Deletes the product image.
        /// </summary>
        //[TestMethod]
        public void DeleteProductAllImage()
        {
            //Arrange
            var prj = GetRootProduct();

            //Act/Assert
            Assert.IsTrue(_application.CatalogServices.ProductImages.DeleteForProductId(prj.Bvin));
            //TODO:Need to change function DeleteAdditionalProductImage for set default portalid
        }

        /// <summary>
        /// Merges the product image.
        /// </summary>
        //[TestMethod]
        public void MergeProductImage()
        {
            //Arrange
            var prj = GetRootProduct();
            var lstimg = _irepoproductimg.GetMergeProductImage();
            var lstmergeimg = (from productImage in lstimg let img = prj.Images.FirstOrDefault(x => x.FileName.Equals(productImage.FileName)) select img ?? productImage).ToList();

            //Act
            _application.CatalogServices.ProductImages.MergeList(prj.Bvin, lstmergeimg);
            //TODO:Need to change DeleteAdditionalProductImage function for CI
            var prj1 = GetRootProduct();

            //Assert
            Assert.AreNotEqual(prj.Images.Count, prj1.Images.Count);
        }

        #endregion

    }
}
