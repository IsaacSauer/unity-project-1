using UnityEngine;
using UnityEngine.Sprites;
using UnityEngine.UI;

[AddComponentMenu("UI/Fit Image")]
public class FitImage : Image
{
	public enum FitMode
	{
		Fill, // Stretch to fill (distorts aspect)
		Contain, // Fit inside without cropping
		Cover // Fill with cropping
	}

	[SerializeField] private FitMode _fitMode = FitMode.Fill;

	public FitMode ImageFit
	{
		get => _fitMode;
		set
		{
			_fitMode = value;
			SetVerticesDirty();
		}
	}

#if UNITY_EDITOR
	protected override void OnValidate()
	{
		base.OnValidate();
		SetVerticesDirty();
	}
#endif

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		if (overrideSprite == null)
		{
			base.OnPopulateMesh(vh);
			return;
		}

		vh.Clear();

		Rect rect = GetPixelAdjustedRect();
		Vector4 uv = DataUtility.GetOuterUV(overrideSprite);
		Vector2 spriteSize = overrideSprite.rect.size;

		float drawWidth = rect.width;
		float drawHeight = rect.height;

		float spriteAspect = spriteSize.x / spriteSize.y;
		float rectAspect = rect.width / rect.height;

		float scale = 1f;

		switch (_fitMode)
		{
			case FitMode.Fill:
				drawWidth = rect.width;
				drawHeight = rect.height;
				break;

			case FitMode.Contain:
				if (rectAspect > spriteAspect)
					scale = rect.height / spriteSize.y;
				else
					scale = rect.width / spriteSize.x;

				drawWidth = spriteSize.x * scale;
				drawHeight = spriteSize.y * scale;
				break;

			case FitMode.Cover:
				if (rectAspect > spriteAspect)
					scale = rect.width / spriteSize.x;
				else
					scale = rect.height / spriteSize.y;

				drawWidth = spriteSize.x * scale;
				drawHeight = spriteSize.y * scale;
				break;
		}

		float xOffset = (rect.width - drawWidth) * 0.5f + rect.x;
		float yOffset = (rect.height - drawHeight) * 0.5f + rect.y;

		Vector2 posMin = new Vector2(xOffset, yOffset);
		Vector2 posMax = new Vector2(xOffset + drawWidth, yOffset + drawHeight);

		Vector2 uvMin = new Vector2(uv.x, uv.y);
		Vector2 uvMax = new Vector2(uv.z, uv.w);

		vh.AddVert(new Vector3(posMin.x, posMin.y), color, new Vector2(uvMin.x, uvMin.y));
		vh.AddVert(new Vector3(posMin.x, posMax.y), color, new Vector2(uvMin.x, uvMax.y));
		vh.AddVert(new Vector3(posMax.x, posMax.y), color, new Vector2(uvMax.x, uvMax.y));
		vh.AddVert(new Vector3(posMax.x, posMin.y), color, new Vector2(uvMax.x, uvMin.y));

		vh.AddTriangle(0, 1, 2);
		vh.AddTriangle(2, 3, 0);
	}
}