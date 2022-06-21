#pragma checksum "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "afcfe7f97a15f5c1b2aca8b318585de61354a93d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Predict_Index), @"mvc.1.0.view", @"/Views/Predict/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\_ViewImports.cshtml"
using SmartBudget;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\_ViewImports.cshtml"
using SmartBudget.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"afcfe7f97a15f5c1b2aca8b318585de61354a93d", @"/Views/Predict/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8e455b6be2716e7b98ef692c01c5a88f80c8738a", @"/Views/_ViewImports.cshtml")]
    public class Views_Predict_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-danger"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Expense", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-success"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Income", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 4 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
  
    var b0 = TempData["B0"] as string;
    var b1 = TempData["B1"] as string;
    var predictedBalanceFirst = TempData["PredictedBalanceFirst"] as string;
    var nextPeriodFirst = TempData["NextPeriodFirst"] as string;
    var predictedBalanceSecond = TempData["PredictedBalanceSecond"] as string;
    var nextPeriodSecond = TempData["NextPeriodSecond"] as string;
    var predictedBalanceThird = TempData["PredictedBalanceThird"] as string;
    var nextPeriodThird = TempData["NextPeriodThird"] as string;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afcfe7f97a15f5c1b2aca8b318585de61354a93d5609", async() => {
                WriteLiteral(@"
    <script>
window.onload = function () {

var chart = new CanvasJS.Chart(""chartContainer"", {
	animationEnabled: true,
	title: {
		text: ""Balance By Year and Month""
	},
	toolTip: {
		shared: true
	},
	data: [{
        markerColor: ""#00003f"",
		type: ""line"",
		name: ""Balance"",
		showInLegend: true,
		dataPoints: ");
#nullable restore
#line 32 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
               Write(Html.Raw(ViewBag.DataPoints));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\t}]\r\n});\r\nchart.render();\r\n\r\n}\r\n    </script>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afcfe7f97a15f5c1b2aca8b318585de61354a93d7217", async() => {
                WriteLiteral("\r\n    <div class=\"row h-100\">\r\n");
#nullable restore
#line 42 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
         if (string.IsNullOrEmpty(predictedBalanceFirst))
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"mt-4\">\r\n                <p>\r\n                    Oops, you don\'t have any records yet. Please add\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afcfe7f97a15f5c1b2aca8b318585de61354a93d7916", async() => {
                    WriteLiteral("expenses");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" and\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afcfe7f97a15f5c1b2aca8b318585de61354a93d9462", async() => {
                    WriteLiteral("incomes");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                    in order to use this functionality.\r\n                </p>\r\n            </div>\r\n");
#nullable restore
#line 52 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
        }
        else
        {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"col align-self-center\">\r\n                <div class=\"row mt-2\">\r\n                    <div id=\"chartContainer\" style=\"height: 370px; width: 100%;\"></div>\r\n                    <p>\r\n                        It seems that there is a ");
#nullable restore
#line 59 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                                            Write(b1);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
#nullable restore
#line 60 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                         if (Double.Parse(b1) < 0)
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <span class=\"text-danger\">decrease</span>\r\n");
#nullable restore
#line 63 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <span class=\"text-success\">increase</span>\r\n");
#nullable restore
#line 67 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                        }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                        in your balance.
                    The predicted balances are present in the chart.</p>
                    <div class=""row justify-content-center"">
                        <table class=""table border-1 w-50 text-center"">
                            <tr>
                                <th>Month</th>
                                <th>Calculated value</th>
                            </tr>
                            <tr>
                                <td>");
#nullable restore
#line 77 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                               Write(nextPeriodFirst);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 78 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                               Write(predictedBalanceFirst);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <td>");
#nullable restore
#line 81 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                               Write(nextPeriodSecond);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 82 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                               Write(predictedBalanceSecond);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                            </tr>\r\n                            <tr>\r\n                                <td>");
#nullable restore
#line 85 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                               Write(nextPeriodThird);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                <td>");
#nullable restore
#line 86 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
                               Write(predictedBalanceThird);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>
                            </tr>
                        </table>
                    </div>
                    <p>
                        <em>
                            If you don't see partcular month in the chart, please provide information for the
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afcfe7f97a15f5c1b2aca8b318585de61354a93d15367", async() => {
                    WriteLiteral("expenses");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(" and\r\n                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "afcfe7f97a15f5c1b2aca8b318585de61354a93d16922", async() => {
                    WriteLiteral("incomes");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                        </em>\r\n                    </p>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 99 "D:\TU\1_sem_mag\PI\SmartBudget\SmartBudget\Views\Predict\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
                WriteLiteral("    </div>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
