using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Interfaces;

public interface IEventPublisher
{
    Task Publish<TEvent>(TEvent @event) where TEvent : class;
}
