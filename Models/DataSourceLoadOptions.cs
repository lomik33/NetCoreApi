using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

//[ModelBinder(typeof(DataSourceLoadOptionsHttpBinder))]
public class DataSourceLoadOptions : DataSourceLoadOptionsBase{

}
public class DataSourceLoadOptionsHttpBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var options = new DataSourceLoadOptions();
        DataSourceLoadOptionsParser.Parse(options, key => bindingContext.ValueProvider.GetValue(key).FirstOrDefault());
        //bindingContext.Model = options;
        bindingContext.Result = ModelBindingResult.Success(options);
        return Task.CompletedTask;

    }
}