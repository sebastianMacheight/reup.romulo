using Newtonsoft.Json.Linq;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.dataSchemas
{
    public static class RomuloInternalSchema
    {
        public static readonly JObject materialChangeInfo = new()
        {
            { "type", DataValidator.objectType },
            { "properties", new JObject
                {
                    { "material_url", DataValidator.stringSchema },
                    { "object_ids",  DataValidator.CreateArraySchema(new JObject[] { DataValidator.stringSchema })},
                }
            },
            { "required", new JArray { "material_url", "object_ids" } }
        };

        public static readonly JObject sceneStateSchema = new()
        {
            { "type", DataValidator.objectType },
            { "properties", new JObject
                {
                    { "id", DataValidator.stringSchema },
                    { "appearance", sceneStateAppearanceSchema },
                    { "children", DataValidator.CreateArraySchema(new JObject[] { sceneStateSchema }) },
                }
            },
            { "required", new JArray { "id" } }
        };

        public static readonly JObject sceneStateAppearanceSchema = new()
        {
            { "type", DataValidator.objectType },
            { "properties", new JObject
                {
                    { "color", DataValidator.stringSchema },
                    { "material_id", DataValidator.stringSchema}
                }
            }
        };
    }
}
