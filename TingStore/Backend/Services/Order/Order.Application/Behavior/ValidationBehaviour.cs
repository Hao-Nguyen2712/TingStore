using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Order.Application.Validators;

namespace Order.Application.Behavior
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any()){
                var context = new ValidationContext<TRequest>(request);
                var validationResult = Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));

                var failure = validationResult.Result.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failure.Count() > 0)
                {
                    throw new ValidatorException(failure);
                }
            }
            return await next();
        }
    }
}
