using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace Cardoni;

public partial class Animation : Sprite2D
{

    
	
	public (float, int)[] Frames { get; set; }

	public int Index { get; set; }

	public int Count { get; set; }

	// Play an animation with custom frames and durations
	public void Play(Texture2D texture, int hframes, int vframes, (float, int)[] frames, int count)
	{
		Texture = texture;
		Hframes = hframes;
		Vframes = vframes;
		Frames = frames;

		Index = 0;
		Count = count;
		(float time, Frame) = Frames[0];
		new ProcessExpiring(time, OnExpire);
	}


	public void Play(Texture2D texture, int hframes, int vframes, float duration)
	{
		List<(float, int)> frames = new();
		for (int i = 0; i < hframes * vframes; i++)
		{
			frames.Add((duration, i));
		}
		Play(texture, hframes, vframes, frames.ToArray(), 1);
	}

	

	void OnExpire()
	{
		Index++;
		if (Index >= Frames.Length)
		{
			if (Count == 1)
			{
				SpecialState.Instance.Animations.Remove(this);
				return;
			}
			else if (Count > 1)
			{
				Count--;
			}
			Index = 0;
		}

		(float time, Frame) = Frames[Index];
		new ProcessExpiring(time, OnExpire);
	}
}
