using UnityEditor;
using UnityEngine.UIElements;

namespace EditorAttributes.Editor
{
	[CustomPropertyDrawer(typeof(PropertyWidthAttribute))]
    public class PropertyWidthDrawer : PropertyDrawerBase
    {
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var propertyWidthAttribute = attribute as PropertyWidthAttribute;

			var root = new VisualElement();
			var propertyField = CreatePropertyField(property);

			root.Add(propertyField);

			ExecuteLater(propertyField, () => propertyField.Q<Label>().style.marginRight = propertyWidthAttribute.WidthOffset);

			return root;
		}
    }
}
