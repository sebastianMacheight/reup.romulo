using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

using ReupVirtualTwin.helpers;
using Newtonsoft.Json.Linq;


public class DataValidatorTest : MonoBehaviour
{
    [Test]
    public void ValidateStringAndIntSchema_should_success()
    {
        Dictionary<string, object> schemaKeys = new Dictionary<string, object>
        {
            { "name", JTokenType.String },
            { "age", JTokenType.Integer }
        };

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "name", "John Doe" },
            { "age", 25 }
        };

        bool result = DataValidator.ValidateObjectToSchemaKeys(data, schemaKeys);

        Assert.IsTrue(result);

    }

    [Test]
    public void IfNotAnInt_should_fail()
    {
        Dictionary<string, object> schemaKeys = new Dictionary<string, object>
        {
            { "name", JTokenType.String },
            { "age", JTokenType.Integer }
        };

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "name", "John Doe" },
            { "age", "25" }
        };

        bool result = DataValidator.ValidateObjectToSchemaKeys(data, schemaKeys);

        Assert.IsFalse(result);
    }

    [Test]
    public void NestedValidation_should_success()
    {
        Dictionary<string, object> schemaKeys = new Dictionary<string, object>
        {
            { "name", JTokenType.String },
            { "age", JTokenType.Integer },
            { "nested_object", new Dictionary<string, object>()
                {
                    { "nested_name", JTokenType.String },
                    { "nested_age", JTokenType.Integer }
                }
            }
        };
        Dictionary<string, object> nestedData = new Dictionary<string, object>
        {
            { "nested_name", "Jane Doe" },
            { "nested_age", 30 }
        };
        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "name", "John Doe" },
            { "age", 25 },
            { "nested_object", nestedData }
        };
        bool result = DataValidator.ValidateObjectToSchemaKeys(data, schemaKeys);
        Assert.IsTrue(result);
    }
}
