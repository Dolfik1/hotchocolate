﻿using System;
using System.Threading.Tasks;
using HotChocolate.Language;

namespace HotChocolate.Execution
{
    internal sealed class ExceptionMiddleware
    {
        private readonly QueryDelegate _next;
        private readonly IErrorHandler _errorHandler;

        public ExceptionMiddleware(
            QueryDelegate next,
            IErrorHandler errorHandler)
        {
            _next = next
                ?? throw new ArgumentNullException(nameof(next));
            _errorHandler = errorHandler
                ?? throw new ArgumentNullException(nameof(errorHandler));
        }

        public async Task InvokeAsync(IQueryContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (QueryException ex)
            {
                context.Exception = ex;
                context.Result = QueryResult.CreateError(
                    _errorHandler.Handle(ex.Errors));
            }
            catch (SyntaxException ex)
            {
                context.Exception = ex;
                context.Result = QueryResult.CreateError(
                    _errorHandler.Handle(ex, builder => builder
                        .SetMessage(ex.Message)
                        .AddLocation(ex.Line, ex.Column)));
            }
            catch (Exception ex)
            {
                context.Exception = ex;
                context.Result = QueryResult.CreateError(
                    _errorHandler.Handle(ex));
            }
        }
    }
}

