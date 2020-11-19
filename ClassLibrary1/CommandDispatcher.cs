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

using System;
using System.Threading.Tasks;
using ConsoleApp1;
using MediatR;

namespace ClassLibrary1
{
    public class CommandDispatcher
    {
        private readonly IMediator _mediator;

        public CommandDispatcher(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task DispatchAsync<TRequest>(TRequest request)
            where TRequest : IHubRequest
        {
            var targetType = typeof(HubRequestWrapper<>);

            var instance = Activator.CreateInstance(targetType.MakeGenericType(typeof(TRequest)), request);

            await _mediator.Send((HubRequestWrapper<TRequest>) instance);
        }
    }
}