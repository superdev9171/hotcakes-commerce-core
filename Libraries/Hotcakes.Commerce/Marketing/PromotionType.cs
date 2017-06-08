﻿#region License

// Distributed under the MIT License
// ============================================================
// Copyright (c) 2016 Hotcakes Commerce, LLC
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

namespace Hotcakes.Commerce.Marketing
{
    /// <summary>
    ///     Types of promotions in the application.
    /// </summary>
    public enum PromotionType
    {
        /// <summary>
        ///     Should never be used.
        /// </summary>
        Unknown = 0,

        /// <summary>
        ///     A sale on one or many items in the store.
        /// </summary>
        Sale = 1,

        /// <summary>
        ///     An offer where qualifications must be met to receive the promotion.
        /// </summary>
        [Obsolete("Use one of the following Promotion Types instead: OfferForLineItems, OfferForOrder, OfferForShipping"
            )] Offer = 2,

        /// <summary>
        ///     Promotion exclusively for affiliates.
        /// </summary>
        Affiliate = 3,

        /// <summary>
        ///     An offer where line items may potentionally be discounted.
        /// </summary>
        OfferForLineItems = 4,

        /// <summary>
        ///     An offer that will potentially be applied at the order level, such as discounting the subtotal.
        /// </summary>
        OfferForOrder = 5,

        /// <summary>
        ///     Offers that affect shipping in some way.
        /// </summary>
        OfferForShipping = 6,

        /// <summary>
        ///     Offers that can add free item to an order.
        /// </summary>
        OfferForFreeItems = 7,

        /// <summary>
        ///     Neither a sale or an offer, but a product-level discount for specific quantities
        /// </summary>
        VolumeDiscount = 8
    }
}