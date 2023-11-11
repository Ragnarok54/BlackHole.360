using System.Reflection;

namespace BlackHole._360.UnitTests;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public void TestMethod1()
    {
    }
}


[TestFixture]
public class NullableConventionsTests
{
    [Test]
    public void GivenEvents_WhenValidateNullableProperties_ThenPropertiesValidated()
    {
        // Arrange
        var assembly = typeof(PurchaseOrderCreated).Assembly;
        var events = assembly.GetTypes().Where(x => x.BaseType == typeof(Event) || x.BaseType == typeof(ModifiedEvent));

        // Assert & Arrange
        ValidateProperties(events);
    }

    [Test]
    public void GivenEntities_WhenValidateNullableProperties_ThenPropertiesValidated()
    {
        // Arrange
        var assembly = typeof(PurchaseOrder).Assembly;
        var a = assembly.GetTypes().ToList();
        var entities = assembly.GetTypes().Where(x => x.GetAllInterfaces().Contains(typeof(IRichEntity))).ToList();

        // Assert & Arrange
        ValidateProperties(entities);
    }

    private static void ValidateProperties(IEnumerable<Type> types)
    {
        var excludedProperties = GetExcludedProperties();

        Assert.Multiple(() =>
        {
            foreach (var type in types)
            {
                var properties = type.GetProperties();
                properties = properties.Where(p => !excludedProperties.Contains(p.Name)).ToArray();

                foreach (var property in properties)
                {
                    var isPropertyValid = IsPropertyValid(property);

                    Assert.That(isPropertyValid, Is.True, $"Property {property.Name} from {type.Name} is not nullable.");
                }
            }
        });
    }

    private static bool IsPropertyValid(PropertyInfo propertyInfo)
    {
        return !propertyInfo.PropertyType.IsValueType || propertyInfo.PropertyType.IsNullableType();
    }

    private static IEnumerable<string> GetExcludedProperties()
    {
        return new List<string>
        {
            nameof(IRichEntity.IndirectCreated)
  };
    }
}