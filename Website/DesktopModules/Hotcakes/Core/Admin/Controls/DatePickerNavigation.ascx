﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePickerNavigation.ascx.cs" Inherits="Hotcakes.Modules.Core.Admin.Controls.DatePickerNavigation" %>

<asp:TextBox ID="radDatePicker" CssClass="DatePickerNav" AutoPostBack="true" runat="server" TextMode="Date"/>
&nbsp;
<asp:LinkButton ID="lnkPrev" Text="Prev" CssClass="hcIconLeft hcDatePickerNavigation" runat="server" />
&nbsp;
<asp:LinkButton ID="lnkNext" Text="Next" CssClass="hcIconRight hcDatePickerNavigation" runat="server" />

