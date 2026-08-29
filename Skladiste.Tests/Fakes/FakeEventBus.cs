using System;
using System.Collections.Generic;
using Skladiste.SharedKernel;

namespace Skladiste.Tests.Fakes;

public class FakeEventBus : IEventBus
{
    public List<object> ObjavljeniDogadjaji { get; } = new();

    public void Publish<T>(T dogadjaj) => ObjavljeniDogadjaji.Add(dogadjaj!);
    public void Subscribe<T>(Action<T> handler) { }
}
