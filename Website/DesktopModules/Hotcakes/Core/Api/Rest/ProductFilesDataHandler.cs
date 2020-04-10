﻿#region License

// Distributed under the MIT License
// ============================================================
// Copyright (c) 2019 Hotcakes Commerce, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
// THE SOFTWARE.

#endregion

using System;
using System.Collections.Specialized;
using Hotcakes.Commerce;
using Hotcakes.Commerce.Storage;
using Hotcakes.CommerceDTO.v1;
using Hotcakes.Web;

namespace Hotcakes.Modules.Core.Api.Rest
{
    [Serializable]
    public class ProductFilesDataHandler : BaseRestHandler
    {
        public ProductFilesDataHandler(HotcakesApplication app)
            : base(app)
        {
        }

        // List or Find Single
        public override string GetAction(string parameters, NameValueCollection querystring)
        {
            var response = new ApiResponse<bool>();
            response.Errors.Add(new ApiError("NOTSUPPORTED", "GET method is not supported for this object."));
            response.Content = false;
            var data = string.Empty;
            data = Json.ObjectToJson(response);
            return data;
        }

        // Create or Update
        public override string PostAction(string parameters, NameValueCollection querystring, string postdata)
        {
            var data = string.Empty;
            var bvin = FirstParameter(parameters);
            var fileName = querystring["filename"];
            var description = querystring["description"];
            var firstPart = querystring["first"];

            var response = new ApiResponse<bool>();

            byte[] postedData = null;
            try
            {
                if (firstPart == "1")
                {
                    var existing = HccApp.CatalogServices.ProductFiles.Find(bvin);
                    if (existing == null || existing.Bvin == string.Empty)
                    {
                        existing.Bvin = bvin;
                        existing.FileName = fileName;
                        existing.ShortDescription = description;
                        existing.StoreId = HccApp.CurrentStore.Id;
                        HccApp.CatalogServices.ProductFiles.Create(existing);
                    }
                }

                postedData = Json.ObjectFromJson<byte[]>(postdata);

                var diskFileName = bvin + "_" + fileName + ".config";
                if (postedData != null)
                {
                    if (postedData.Length > 0)
                    {
                        if (firstPart == "1")
                        {
                            response.Content = DiskStorage.FileVaultUploadPartial(HccApp.CurrentStore.Id, diskFileName,
                                postedData, true);
                        }
                        else
                        {
                            response.Content = DiskStorage.FileVaultUploadPartial(HccApp.CurrentStore.Id, diskFileName,
                                postedData, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.Errors.Add(new ApiError("EXCEPTION", ex.Message));
                return Json.ObjectToJson(response);
            }

            data = Json.ObjectToJson(response);
            return data;
        }

        public override string DeleteAction(string parameters, NameValueCollection querystring, string postdata)
        {
            var response = new ApiResponse<bool>();
            response.Errors.Add(new ApiError("NOTSUPPORTED", "Delete method is not supported for this object."));
            response.Content = false;
            var data = string.Empty;
            data = Json.ObjectToJson(response);
            return data;
        }
    }
}