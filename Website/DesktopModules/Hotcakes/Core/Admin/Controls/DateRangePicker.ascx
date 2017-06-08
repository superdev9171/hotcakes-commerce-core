<%@ Control Language="C#" AutoEventWireup="True" Inherits="Hotcakes.Modules.Core.Admin.Controls.DateRangePicker" CodeBehind="DateRangePicker.ascx.cs" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="<%=FormItemCssClass %>">
    <asp:Label CssClass="hcLabelShort" ID="lblDateRangeLabel" resourcekey="DateRange" AssociatedControlID="lstRangeType" runat="server" />
    <asp:DropDownList runat="server" ID="lstRangeType" AutoPostBack="True"
        OnSelectedIndexChanged="lstRangeType_SelectedIndexChanged">
        <asp:ListItem Value="9" resourcekey="AllDates" />
        <asp:ListItem Value="1" resourcekey="Today" />
        <asp:ListItem Value="12" resourcekey="Yesterday" />
        <asp:ListItem Value="2" resourcekey="ThisWeek" />
        <asp:ListItem Value="3" resourcekey="LastWeek" />
        <asp:ListItem Value="10" resourcekey="ThisMonth" />
        <asp:ListItem Value="11" resourcekey="LastMonth" />
        <asp:ListItem Value="4" resourcekey="Last31Days" />
        <asp:ListItem Value="5" resourcekey="Last60Days" />
        <asp:ListItem Value="6" resourcekey="Last120Days" />
        <asp:ListItem Value="7" resourcekey="YearToDate" />
        <asp:ListItem Value="8" resourcekey="LastYear" />
        <asp:ListItem Value="99" resourcekey="CustomDateRange" />
    </asp:DropDownList>
</div>

<asp:Panel runat="server" ID="pnlCustom" Visible="false">
    <div class="<%=FormItemCssClass %>">
        <label class="hcLabelShort"><%=Localization.GetString("Start") %></label>
        <telerik:RadDatePicker ID="radStartDate"  runat="server"/>
    </div>
    <div class="<%=FormItemCssClass %>">
        <label class="hcLabelShort"><%=Localization.GetString("End") %></label>
        <telerik:RadDatePicker ID="radEndDate" runat="server" />
        <asp:LinkButton ID="btnShow" CssClass="hcButton hcDatePickerButton" resourcekey="Show" runat="server" />
    </div>
</asp:Panel>
