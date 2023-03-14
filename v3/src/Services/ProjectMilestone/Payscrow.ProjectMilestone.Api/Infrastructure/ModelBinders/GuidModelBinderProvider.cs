using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Threading.Tasks;

namespace Payscrow.ProjectMilestone.Api.Infrastructure.ModelBinders
{
    public class GuidModelBinderProvider : IModelBinderProvider
    {
        /// <summary>
        /// This method implements a new model binder for GUID types only.
        /// </summary>
        /// <param name="context">Contains the model binder provider context.</param>
        /// <returns>Returns a model binder if the type is for GUID</returns>
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            IModelBinder result = null;

            if (context?.Metadata.ModelType == typeof(Guid))
            {
                result = new GuidModelBinder();
            }

            return result;
        }
    }

    /// <summary>
    /// This class overrides the current GUID model binder and makes it a bit smarter in situations where the value is empty or invalid.
    /// The default returned value is to return an Empty Guid which for our purposes should mean the value passed was empty.
    /// </summary>
    /// <remarks>
    /// The GUID binder will cause an invalid model error if no GUID value is specified in the Request data. This is by default a situation that
    /// arises a lot in regard to an optional Guid identity value not being specified in Request.
    /// </remarks>
    public class GuidModelBinder : IModelBinder
    {
        /// <summary>
        /// This call is made to execute the binding.
        /// </summary>
        /// <param name="bindingContext">Contains the model binding context.</param>
        /// <returns>Returns the parsed GUID using TryParse instead of the harsher </returns>
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            ValueProviderResult parameter = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            Guid result = Guid.Empty;
            Guid.TryParse(parameter.FirstValue, out result);
            return Task.FromResult(result);
        }
    }
}
