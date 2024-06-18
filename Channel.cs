using Microsoft.CodeAnalysis.CSharp.Syntax;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace rocket_bot;

public class Channel<T> where T : class
{
	private List<T> Values = new();
	readonly object locker = new();

	/// <summary>
	/// Возвращает элемент по индексу или null, если такого элемента нет.
	/// При присвоении удаляет все элементы после.
	/// Если индекс в точности равен размеру коллекции, работает как Append.
	/// </summary>
	public T this[int index]
	{
		get
		{
			lock (locker)
			{
                if (index < 0 || index >= this.Values.Count)
                {
                    return null;
                }
                return this.Values[index];
            }
        }
		set
		{
			lock(locker)
			{
                if (index > this.Values.Count || index < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(index), "index out of collection range");
                }
                else if(index == this.Values.Count)
                {
                    var newCollection = new List<T>(index + 1);
					newCollection.AddRange(this.Values);
                    newCollection.Add(value);
                    this.Values = newCollection;
                }
				else
				{
                    var newCollection = new List<T>(index + 1);
                    for (int i = 0; i <= index; i++)
					{
						newCollection.Add(this.Values[i]);
					}
                    newCollection[index] = value;
                    this.Values = newCollection;
                }
            }
        }
	}

	/// <summary>
	/// Возвращает последний элемент или null, если такого элемента нет
	/// </summary>
	public T LastItem()
	{
		lock(locker)
		{
            return this[this.Values.Count - 1];
        }
    }

	/// <summary>
	/// Добавляет item в конец только если lastItem является последним элементом
	/// </summary>
	public void AppendIfLastItemIsUnchanged(T item, T knownLastItem)
	{
		lock(locker)
		{
            if (this.LastItem() == knownLastItem)
            {
                this[this.Values.Count] = item;
            }
        }
    }

	/// <summary>
	/// Возвращает количество элементов в коллекции
	/// </summary>
	public int Count
	{
		get
		{
			lock(locker)
			{
                return this.Values.Count;
            }
        }
	}
}