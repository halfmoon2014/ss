﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcPager.aspx.cs" Inherits="AjaxHandler_ProcPager" %>

<div id="MyDivTable"  runat="server" class="MyDivTableClass"  ></div>
<ctrlPager:ProcPager ID="pagerTest" RepeaterId="MyDivTable" runat="server" />