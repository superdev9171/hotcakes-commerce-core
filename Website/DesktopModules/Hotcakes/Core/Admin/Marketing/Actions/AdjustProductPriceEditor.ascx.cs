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

using System.Web.UI.WebControls;
using Hotcakes.Commerce.Marketing;
using Hotcakes.Commerce.Marketing.PromotionActions;
using Hotcakes.Commerce.Utilities;
using Hotcakes.Common.Dnn;
using Hotcakes.Modules.Core.Admin.AppCode;

namespace Hotcakes.Modules.Core.Admin.Marketing.Actions
{
    public partial class AdjustProductPrice : BaseActionControl
    {
        private ProductPriceAdjustment TypedAction
        {
            get { return Action as ProductPriceAdjustment; }
        }

        public override void LoadAction()
        {
            if (lstAdjustmentType.Items.Count == 0)
            {
                lstAdjustmentType.Items.Add(new ListItem(Localization.GetString("Amount"), "0"));
                lstAdjustmentType.Items.Add(new ListItem(Localization.GetString("Percent"), "1"));
            }

            AmountField.Text = TypedAction.Amount.ToString();
            lstAdjustmentType.SelectedValue = TypedAction.AdjustmentType == AmountTypes.Percent ? "1" : "0";
        }

        public override bool SaveAction()
        {
            TypedAction.AdjustmentType = lstAdjustmentType.SelectedValue == "1"
                ? AmountTypes.Percent
                : AmountTypes.MonetaryAmount;
            TypedAction.Amount = AmountField.Text.ConvertTo(TypedAction.Amount);

            if (TypedAction.AdjustmentType == AmountTypes.MonetaryAmount)
            {
                TypedAction.Amount = Money.RoundCurrency(TypedAction.Amount);
            }

            return UpdatePromotion();
        }
    }
}