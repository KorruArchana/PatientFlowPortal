using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace EMIS.PatientFlow.Web.Helper
{
    public static class DataListInput
    {
        public static IHtmlString  DataListInputFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IEnumerable<string> optionsList, string optionLabel, object htmlAttributes)
        {
            StringBuilder html = new StringBuilder();
            TagBuilder input = new TagBuilder("input"    );
            var name = ExpressionHelper.GetExpressionText(expression);
            input.Attributes.Add("id", name);
            input.Attributes.Add("name", name);

            TagBuilder dataList = new TagBuilder("datalist");
            dataList.Attributes.Add("id", "datalist_" + name);
            dataList.Attributes.Add("name", "datalist_" + name);
            input.Attributes.Add("value", optionLabel);
            input.Attributes.Add("list", dataList.Attributes["id"]);
            var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            input.MergeAttributes(attributes, true);
            string optionTag = "<option value=\"{0}\"></option>";
            StringBuilder optionsHtml = new StringBuilder();
            foreach (var entry in optionsList)
            {
                var option = string.Format(optionTag, entry);
                optionsHtml.AppendLine(option);
            }
            dataList.InnerHtml = optionsHtml.ToString();
            html.Append(input.ToString());
            html.Append(dataList.ToString());
            System.Diagnostics.Debug.WriteLine(html.ToString());
            return MvcHtmlString.Create(html.ToString());
        }
    }
}