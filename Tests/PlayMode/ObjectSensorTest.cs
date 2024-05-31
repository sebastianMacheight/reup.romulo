using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using System.Threading.Tasks;
using ReupVirtualTwin.controllers;
using ReupVirtualTwin.webRequestersInterfaces;
using Tests.PlayMode.Mocks;
using ReupVirtualTwin.managerInterfaces;
using ReupVirtualTwin.enums;
using Newtonsoft.Json.Linq;
using ReupVirtualTwin.helperInterfaces;
using ReupVirtualTwin.helpers;

namespace ReupVirtualTwinTests.helpers
{
    public class ObjectSensorTest : MonoBehaviour
    {
        GameObject sensorGameObject;
        GameObject testObject;
        ObjectSensor objectSensor;
        IRayProvider rayProvider;
        ObjectSelectorMock objectSelector;

        class RayProviderMock : MonoBehaviour, IRayProvider
        {
            public Ray GetRay()
            {
                return new Ray(Vector3.zero, Vector3.forward);
            }
        }
        class ObjectSelectorMock : MonoBehaviour, IObjectSelector
        {
            public GameObject returnGameObject;
            public GameObject GetObject(Ray ray)
            {
                return returnGameObject;
            }
        }

        [SetUp]
        public void SetUp()
        {
            sensorGameObject = new GameObject("sensorGameObject");
            rayProvider = sensorGameObject.AddComponent<RayProviderMock>();
            objectSelector = sensorGameObject.AddComponent<ObjectSelectorMock>();
            objectSensor = sensorGameObject.AddComponent<ObjectSensor>();
        }
        [TearDown]
        public void TearDown()
        {
            Destroy(sensorGameObject);
            Destroy(testObject);
        }

        [Test]
        public void ShouldSenseNoObject()
        {
            GameObject sensedObject = objectSensor.Sense();
            Assert.IsNull(sensedObject);
        }

        [Test]
        public void ShouldSenseObjectObtainedByObjectSelector()
        {
            testObject = new GameObject("testObject");
            objectSelector.returnGameObject = testObject;
            GameObject sensedObject = objectSensor.Sense();
            Assert.AreEqual(testObject, sensedObject);
        }

    }
}
