#pragma checksum "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\Home\Galeri.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "afedd2612e5f0f429d815ec529e8312d887fe3876ee6962c22baf386b072b85e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Customer_Views_Home_Galeri), @"mvc.1.0.view", @"/Areas/Customer/Views/Home/Galeri.cshtml")]
namespace AspNetCore
{
    #line default
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\_ViewImports.cshtml"
using Cafe

#nullable disable
    ;
#nullable restore
#line 2 "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\_ViewImports.cshtml"
using Cafe.Models

#line default
#line hidden
#nullable disable
    ;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"afedd2612e5f0f429d815ec529e8312d887fe3876ee6962c22baf386b072b85e", @"/Areas/Customer/Views/Home/Galeri.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"60571d86adac999e7d05c08ab9059371e22c96efd2be152d593ab943143b06eb", @"/Areas/Customer/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Areas_Customer_Views_Home_Galeri : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Cafe.Models.Galeri>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\Home\Galeri.cshtml"
  
    ViewData["Title"] = "Galeri";
    Layout = "~/Views/Shared/_ALoyut.cshtml";

#line default
#line hidden
#nullable disable

            WriteLiteral(@"
<!-- Title Page -->
<section class=""bg-title-page flex-c-m p-t-160 p-b-80 p-l-15 p-r-15"" style=""background-image: url(/Site/images/bg-title-page-02.jpg);"">
	<h2 class=""tit6 t-center"">
		Galeri
	</h2>
</section>



<!-- Gallery -->
<div class=""section-gallery p-t-118 p-b-100"">

	<div class=""wrap-gallery isotope-grid flex-w p-l-25 p-r-25"">
		<!-- - -->
");
#nullable restore
#line 21 "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\Home\Galeri.cshtml"
   foreach (var item in Model)
		{

#line default
#line hidden
#nullable disable

            WriteLiteral("\t\t\t<div class=\"item-gallery isotope-item bo-rad-10 hov-img-zoom events guests\">\r\n\t\t\t\t<img");
            BeginWriteAttribute("src", " src=\"", 624, "\"", 641, 1);
            WriteAttributeValue("", 630, 
#nullable restore
#line 24 "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\Home\Galeri.cshtml"
               item.Image

#line default
#line hidden
#nullable disable
            , 630, 11, false);
            EndWriteAttribute();
            WriteLiteral(" alt=\"IMG-GALLERY\" height=\"300\" width=\"150\">\r\n\r\n\r\n\t\t\t</div>\r\n");
#nullable restore
#line 28 "C:\Users\PC\source\repos\Cafe\Cafe\Areas\Customer\Views\Home\Galeri.cshtml"
		}

#line default
#line hidden
#nullable disable

            WriteLiteral("\t\t\r\n\r\n\t\r\n\t\t\r\n\t</div>\r\n\r\n\t\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Cafe.Models.Galeri>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591