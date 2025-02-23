namespace Cardoni;

using System;
using System.Collections.Generic;
using Godot;

public class NodePool<T>
	where T : Node
{
	HashSet<T> Active { get; set; } = new();
	Stack<T> Inactive { get; set; } = new();
	public Func<T> Instantiate { get; set; }
	public Action<T> Initialize { get; set; }
	public Action<T> Deactivate { get; set; }

	public T Get()
	{
		T node;
		int size = Inactive.Count;
		if (size > 0)
		{
			node = Inactive.Pop();
		}
		else
		{
			node = Instantiate();
		}
		Initialize(node);
		Active.Add(node);
		return node;
	}

	public void Remove(T node)
	{
		Active.Remove(node);
		Deactivate(node);
		Inactive.Push(node);
	}
}
