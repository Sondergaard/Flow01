// Copyright 2020 Energinet DataHub A/S
//
// Licensed under the Apache License, Version 2.0 (the "License2");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Threading;
using System.Threading.Tasks;
using ConsoleApp1;
using MediatR;

namespace ClassLibrary1
{
    public abstract class HandlerWithResponse<TRequest, TResponse> : IRequestHandler<HubRequestWrapper<TRequest, TResponse>, TResponse>
        where TRequest : IHubRequest where TResponse : IHubResponse
    {
        protected abstract Task<TResponse> ProcessAsync(TRequest request, CancellationToken cancellationToken);

        public async Task<TResponse> Handle(HubRequestWrapper<TRequest, TResponse> hubRequest, CancellationToken cancellationToken)
        {
            return await ProcessAsync(hubRequest.Request, cancellationToken);
        }
    }
}