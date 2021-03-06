using Content.Shared.GameObjects.Components.Power;
using Content.Shared.Utility;
using SS14.Client.GameObjects;
using SS14.Client.Interfaces.GameObjects.Components;
using SS14.Shared.Interfaces.GameObjects;
using SS14.Shared.Utility;
using YamlDotNet.RepresentationModel;

namespace Content.Client.GameObjects.Components.Power
{
    public class PowerCellVisualizer2D : AppearanceVisualizer
    {
        private string _prefix;

        public override void LoadData(YamlMappingNode node)
        {
            base.LoadData(node);

            _prefix = node.GetNode("prefix").AsString();
        }

        public override void InitializeEntity(IEntity entity)
        {
            base.InitializeEntity(entity);

            var sprite = entity.GetComponent<ISpriteComponent>();

            sprite.LayerMapSet(Layers.Charge, sprite.AddLayerState($"{_prefix}_100"));
            sprite.LayerSetShader(Layers.Charge, "unshaded");
        }

        public override void OnChangeData(AppearanceComponent component)
        {
            base.OnChangeData(component);

            var sprite = component.Owner.GetComponent<ISpriteComponent>();
            if (component.TryGetData(PowerCellVisuals.ChargeLevel, out float fraction))
            {
                sprite.LayerSetState(Layers.Charge, $"{_prefix}_{ContentHelpers.RoundToLevels(fraction, 1, 5) * 25}");
            }
        }

        private enum Layers
        {
            Charge
        }
    }
}
