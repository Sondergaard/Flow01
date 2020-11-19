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

namespace ClassLibrary1
{
    public class RequestHandler<TRequest> : BaseRequestHandler<TRequest, IHubResponse>
        where TRequest : IHubRequest
    {
        protected RequestHandler() { }

        protected virtual Task<bool> ValidateAsync(TRequest request, CancellationToken cancellationToken) => Task.FromResult(true);
        protected virtual Task<bool> AcceptAsync(TRequest request, CancellationToken cancellationToken) => Task.FromResult(true);
        protected virtual Task<IHubResponse> RespondAsync(TRequest request, CancellationToken cancellationToken) => Task.FromResult<IHubResponse>(new HubResponse());

        protected override async Task<IHubResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
        {
            if (!await ValidateAsync(request, cancellationToken))
            {
                return new HubResponse();
            }

            if (!await AcceptAsync(request, cancellationToken))
            {
                return new HubResponse();
            }

            return await RespondAsync(request, cancellationToken);
        }
    }
}