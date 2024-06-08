using Newtonsoft.Json.Linq;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwin.dataSchemas
{
    public static class RomuloInternalSchema
    {
        public static JObject materialChangeInfo { get; private set; }
        public static JObject sceneStateSchema { get; private set; }
        public static JObject sceneStateAppearanceSchema { get; private set; }

        static RomuloInternalSchema()
        {
            materialChangeInfo = new()
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

            sceneStateAppearanceSchema = new()
            {
                { "type", DataValidator.objectType },
                { "properties", new JObject
                    {
                        { "color", DataValidator.stringSchema },
                        { "material_id", DataValidator.stringSchema}
                    }
                }
            };

            sceneStateSchema = new()
            {
                { "type", DataValidator.objectType },
                { DataValidator.schemaName, "sceneStateSchema" },
                { "properties", new JObject
                    {
                        { "id", DataValidator.stringSchema },
                        { "appearance", sceneStateAppearanceSchema },
                        { "children", DataValidator.CreateArraySchema(new JObject[]
                            { DataValidator.CreateRefSchema("sceneStateSchema") })
                        },
                    }
                },
                { "required", new JArray { "id" } }
            };

        }


    }
}
