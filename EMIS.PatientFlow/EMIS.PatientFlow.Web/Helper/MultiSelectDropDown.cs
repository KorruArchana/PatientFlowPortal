using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

namespace EMIS.PatientFlow.Web.Helper
{
    public static class MultiSelectDropDown
    {
        public static MvcHtmlString Custom_DropdownList(
            this HtmlHelper helper,
            string name,
            IEnumerable<CustomSelectList> list, 
            object htmlAttributes, 
            string selectedValue, 
            string classname,
            bool isSearch)
        {
            selectedValue = selectedValue.Replace(" ", string.Empty);
            var dropdown = new TagBuilder("select");
            dropdown.Attributes.Add("name", name);
            dropdown.Attributes.Add("id", name);
            dropdown.Attributes.Add("class", classname);
            dropdown.Attributes.Add("multiple", "multiple");
            var options = new StringBuilder();
            if (list != null)
            {
                int position = -1;
                foreach (var item in list)
                {
                    if (selectedValue != string.Empty)
                    {
                        var stringArray = selectedValue.Split(',');
                        position = Array.IndexOf(stringArray, item.DataValueField);
                    }

                    if (isSearch)
                    {
                        options = position > -1
                                      ? options.Append("<option selected='selected' ; value='" + item.DataValueField +
                                                       "*" + item.DataTextField + "'>" + item.DataTextField +
                                                       "</option>")
                                      : options.Append("<option value='" + item.DataValueField + "*" +
                                                       item.DataTextField + "' style='  '>" + item.DataTextField +
                                                       "</option>");
                    }
                    else
                    {
                        options = position > -1 ? options.Append("<option selected='selected' ; value='" + item.DataValueField + "'>" + item.DataTextField + "</option>") : options.Append("<option value='" + item.DataValueField + "' style='  '>" + item.DataTextField + "</option>");
                    }
                }
            }

            dropdown.InnerHtml = options.ToString();
            dropdown.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(dropdown.ToString(TagRenderMode.Normal));
        }
    }
}