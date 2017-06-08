<%@ Control Language="C#" AutoEventWireup="True" Inherits="Hotcakes.Modules.CategoryMenu.Settings" CodeBehind="Settings.ascx.cs" %>
<%@ Register Src="../../../controls/labelcontrol.ascx" TagName="labelcontrol" TagPrefix="dnn" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div class="dnnFormMessage dnnFormInfo">
    <asp:Label ID="Label1" runat="server" resourcekey="SettingsHint" />
</div>
<div class="dnnForm">
    <fieldset>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="ViewLabel" controlname="ViewContentLabel" suffix=":" runat="server" />
            <asp:Label ID="ViewContentLabel" CssClass="dnnFormLabel" runat="server" Text="" />
        </div>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="ViewSelectionLabel" controlname="ViewComboBox" suffix=":" runat="server" />
            <telerik:RadComboBox ID="ViewComboBox" runat="server" Width="250px" Height="150px"
                EnableLoadOnDemand="False" ShowMoreResultsBox="false" EnableVirtualScrolling="false" />
        </div>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="MenuTitleLabel" controlname="TitleField" suffix=":" runat="server" />
            <asp:TextBox ID="TitleField" runat="server" Width="225px"></asp:TextBox>
        </div>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="ModeFieldLabel" controlname="ModeField" suffix=":" runat="server" />
            <telerik:RadComboBox ID="ModeField" runat="server" AutoPostBack="True" Width="225px">
                <Items>
                    <telerik:RadComboBoxItem Value="0" Text="Show Root Categories Only" />
                    <telerik:RadComboBoxItem Value="1" Text="Show All Categories" />
                    <telerik:RadComboBoxItem Value="2" Text="Show Children, Peers and Parents" />
                    <telerik:RadComboBoxItem Value="3" Text="Show All Categories for Current Parent" />
                    <telerik:RadComboBoxItem Value="4" Text="Show Selected Categories" />
                    <telerik:RadComboBoxItem Value="5" Text="Show Children of Selected Category" />
                </Items>
            </telerik:RadComboBox>
        </div>
        <asp:Panel ID="pnlSelectedCategories" runat="server" Visible="False">
            <dnn:labelcontrol id="lblSelectCategories" suffix=":" runat="server" />
            <table>
                <tr>
                    <td>
                        <asp:Label runat="server" ID="lblAvailableCategories"></asp:Label></td>
                    <td></td>
                    <td>
                        <asp:Label runat="server" ID="lblSelectedCategories"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <asp:ListBox ID="lstCategories" Width="200px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                    <td>
                        <div class="dnnFormItem">
                            <asp:Button runat="server" ID="btnRemoveCategory" Text="<<" />
                        </div>
                        <div class="dnnFormItem">
                            <asp:Button runat="server" ID="btnSelectCategory" Text=">>" />
                        </div>
                    </td>
                    <td>
                        <asp:ListBox ID="lstSelectedCategories" Width="200px" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlSelectedChildren" runat="server" Visible="False">
            <dnn:labelcontrol id="lblSelectParent" controlname="ModeField" suffix=":" runat="server" />
            <telerik:RadComboBox ID="ddlParentCategories" runat="server" Width="225px" />
        </asp:Panel>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="HomeLinkFieldLabel" controlname="HomeLinkField" runat="server" />
            <asp:CheckBox ID="HomeLinkField" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="ProductCountLabel" controlname="ProductCountCheckBox" runat="server" />
            <asp:CheckBox ID="ProductCountCheckBox" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="SubCategoryCountLabel" controlname="SubCategoryCountCheckBox" runat="server" />
            <asp:CheckBox ID="SubCategoryCountCheckBox" runat="server" />
        </div>
        <div class="dnnFormItem">
            <dnn:labelcontrol id="MaximumDepthLabel" controlname="MaximumDepth" suffix=":" runat="server" />
            <asp:TextBox ID="MaximumDepth" runat="server"></asp:TextBox>
        </div>
    </fieldset>
</div>

