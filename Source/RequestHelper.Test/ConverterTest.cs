using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using RequestHelper;
using RequestHelper.Models;
using RequestHelper.Test.Helpers;
using RequestHelper.Test.ModelsForConvert;

namespace RequestHelper.Test
{
    [TestClass]
    public class ConverterTest
    {
        #region SimpleModel

        private SimpleModel GetSimpleModel()
        {
            return new SimpleModel()
            {
                IntValue = RandomHelper.RandomInt(),
                TestValue = RandomHelper.RandomString()
            };
        }

        [TestMethod]
        public void SimpleModelAddTest()
        {
            SimpleModel model = GetSimpleModel();

            RequestParameters testParameters = new RequestParameters();
            testParameters.Add("model", model);

            ParametersDictionary expectedParameters = new ParametersDictionary()
            {
                new KeyValuePair<string, string>(
                   $"{nameof(model)}.{nameof(model.TestValue)}",
                   $"{model.TestValue}"),

                new KeyValuePair<string, string>(
                   $"{nameof(model)}.{nameof(model.IntValue)}",
                   $"{model.IntValue}"),
            };

            RequestParameters expectedCollection = new RequestParameters(expectedParameters);

            Assert.AreEqual(testParameters.ToString(), expectedCollection.ToString());
        }

        [TestMethod]
        public void SimpleModelCreateTest()
        {
            SimpleModel model = GetSimpleModel();

            RequestParameters testParameters = RequestParameters.CreateFromModel(model);

            ParametersDictionary expectedParameters = new ParametersDictionary()
            {
                new KeyValuePair<string, string>(
                   $"{nameof(model.TestValue)}",
                   $"{model.TestValue}"),

                new KeyValuePair<string, string>(
                   $"{nameof(model.IntValue)}",
                   $"{model.IntValue}"),
            };

            RequestParameters expectedCollection = new RequestParameters(expectedParameters);

            Assert.AreEqual(testParameters.ToString(), expectedCollection.ToString());
        }

        #endregion


        #region EmptyModel

        private NullableModel GetNullableModel()
        {
            return new NullableModel()
            {
                HeightFrom = RandomHelper.RandomInt()
            };
        }

        [TestMethod]
        public void NullableModelAddTest()
        {
            NullableModel model = GetNullableModel();

            RequestParameters testParameters = new RequestParameters();
            testParameters.Add("model", model);

            ParametersDictionary expectedParameters = new ParametersDictionary()
            {
                new KeyValuePair<string, string>(
                   $"{nameof(model)}.{nameof(model.HeightFrom)}",
                   $"{model.HeightFrom}"),
            };

            RequestParameters expectedCollection = new RequestParameters(expectedParameters);

            Assert.AreEqual(testParameters.ToString(), expectedCollection.ToString());
        }

        [TestMethod]
        public void NullableModelCreateTest()
        {
            NullableModel model = GetNullableModel();

            RequestParameters testParameters = RequestParameters.CreateFromModel(model);

            ParametersDictionary expectedParameters = new ParametersDictionary()
            {
                new KeyValuePair<string, string>(
                   $"{nameof(model.HeightFrom)}",
                   $"{model.HeightFrom}"),
            };

            RequestParameters expectedCollection = new RequestParameters(expectedParameters);

            Assert.AreEqual(testParameters.ToString(), expectedCollection.ToString());
        }

        #endregion
    }
}
