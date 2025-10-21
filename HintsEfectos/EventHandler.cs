using System.Collections.Generic;
using System.Linq;
using System.Text;
using HintServiceMeow.Core.Enum;
using HintServiceMeow.Core.Extension;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using LabApi.Events.Arguments.PlayerEvents;
using LabApi.Events.CustomHandlers;
using LabApi.Features.Wrappers;

namespace HintsEfectos;

public class EventHandler : CustomEventsHandler
{
    private readonly Dictionary<Player, Hint> _playerHints = new Dictionary<Player, Hint>();

    public override void OnPlayerUpdatedEffect(PlayerEffectUpdatedEventArgs ev)
    {
        var player = ev.Player;
        var playerDisplay = PlayerDisplay.Get(player.ReferenceHub);
        
        if (_playerHints.TryGetValue(player, out Hint oldHint))
        {
            
            playerDisplay.RemoveHint(oldHint);
            _playerHints.Remove(player);
        }
        
        var hintBuilder = new StringBuilder();
        if (player.ActiveEffects.Any())
        {
            hintBuilder.AppendLine("<b><color=yellow>Efectos Activos:</color></b>");
            foreach (var efecto in player.ActiveEffects)
            {
                hintBuilder.AppendLine($"- {efecto.GetType().Name}");
            }
        }
        else
        {
            return; 
        }
        
        
        var newHint = new Hint
        {
            Text = hintBuilder.ToString(),
            Alignment = HintAlignment.Left,
            YCoordinate = 750
        };
        
        playerDisplay.AddHint(newHint);
        
        _playerHints[player] = newHint;
    }

    public override void OnPlayerLeft(PlayerLeftEventArgs ev)
    {
        if (_playerHints.ContainsKey(ev.Player))
        {
            _playerHints.Remove(ev.Player);
        }
    }
}