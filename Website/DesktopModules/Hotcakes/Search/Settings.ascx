﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Settings.ascx.cs" Inherits="Hotcakes.Modules.Search.Settings" %>
<%@ Register Src="../../../controls/labelcontrol.ascx" TagName="labelcontrol" TagPrefix="dnn" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<div class="dnnFormMessage dnnFormInfo">
    <asp:Label runat="server" resourcekey="SettingsHint" />
</div>
<div class="dnnForm">
	<fieldset>
		<div class="dnnFormItem">
			<dnn:LabelControl ID="ViewLabel" ControlName="ViewContentLabel" Suffix=":" runat="server" />
			<asp:Label ID="ViewContentLabel" CssClass="dnnFormLabel" runat="server" Text="" />
		</div>
		<div class="dnnFormItem">
			<dnn:LabelControl ID="ViewSelectionLabel" ControlName="ViewComboBox" Suffix=":" runat="server" />
			<telerik:RadComboBox ID="ViewComboBox" runat="server" Width="250px" Height="150px"
				EnableLoadOnDemand="False" ShowMoreResultsBox="false" EnableVirtualScrolling="false"/>
		</div>
		<div class="dnnFormItem">
			<dnn:LabelControl ID="PageSizeLabel" ControlName="ProductPageSizeTextBox" Suffix=":" runat="server" />
			<asp:TextBox ID="PageSizeTextBox" CssClass="dnnFormLabel" runat="server" />
		</div>
        <div class="dnnFormItem">
			<dnn:LabelControl ID="ShowManufactures" ControlName="ShowManufacturesCheckBox" Suffix=":" runat="server" />
			<asp:CheckBox ID="ShowManufacturesCheckBox" CssClass="dnnFormLabel" runat="server" />
		</div>
        <div class="dnnFormItem">
			<dnn:LabelControl ID="ShowVendors" ControlName="ShowVendorsCheckBox" Suffix=":" runat="server" />
			<asp:CheckBox ID="ShowVendorsCheckBox" CssClass="dnnFormLabel" runat="server" />
		</div>
	</fieldset>
</div>
