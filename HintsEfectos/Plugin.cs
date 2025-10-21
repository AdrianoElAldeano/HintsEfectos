using System;
using LabApi.Events.CustomHandlers;
using LabApi.Features;

namespace HintsEfectos
{
    public class Plugin : LabApi.Loader.Features.Plugins.Plugin
    {
        public override string Name => "HintsEfectos";
        public override string Description => "Hint donde te dira los efectos que tienes.";
        public override string Author => "AdrianoElAldeano";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredApiVersion => new Version(LabApiProperties.CompiledVersion);
        private EventHandler EventHandler = new EventHandler();
        
        public override void Enable()
        {
            CustomHandlersManager.RegisterEventsHandler(EventHandler);
        }

        public override void Disable()
        {
            CustomHandlersManager.UnregisterEventsHandler(EventHandler);
        }
    }
}