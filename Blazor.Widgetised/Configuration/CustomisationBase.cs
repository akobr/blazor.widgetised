using System;

namespace Blazor.Widgetised.Configuration
{
    public abstract class CustomisationBase<TModel> : IMergeable<TModel>
    {
        private readonly Type modelType = typeof(TModel);

        public CustomisationBase()
        {
            if (modelType.IsInterface)
            {
                throw new InvalidOperationException("The customisation generic type must be an interface.");
            }

            if (GetType().IsAssignableFrom(modelType))
            {
                throw new InvalidOperationException("The customisation must implement the interface specified as the generic type.");
            }
        }

        public void Merge(TModel content)
        {
            Customisation.Merge(this, content, modelType);
        }
    }
}
