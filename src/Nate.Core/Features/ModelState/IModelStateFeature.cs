using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Nate.Core.Features.ModelState
{
    public interface IModelStateFeature
    {
        ModelStateDictionary ModelState { get; }
    }
}
