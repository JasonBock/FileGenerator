using Reflector.CodeModel;
using System;
using System.Collections;
using System.Collections.Generic;

namespace FileGenerator.Tests.AddIn.Generators
{
	internal sealed class MockModuleCollection : IModuleCollection
	{
		public void Add(IModule value)
		{
			throw new NotImplementedException();
		}

		public void AddRange(ICollection value)
		{
			throw new NotImplementedException();
		}

		public void Clear()
		{
			throw new NotImplementedException();
		}

		public bool Contains(IModule value)
		{
			throw new NotImplementedException();
		}

		public int IndexOf(IModule value)
		{
			throw new NotImplementedException();
		}

		public void Insert(int index, IModule value)
		{
			throw new NotImplementedException();
		}

		public void Remove(IModule value)
		{
			throw new NotImplementedException();
		}

		public void RemoveAt(int index)
		{
			throw new NotImplementedException();
		}

		public IModule this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public void CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		public int Count
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public bool IsSynchronized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public object SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		public IEnumerator GetEnumerator()
		{
			return new ArrayList().GetEnumerator();
		}
	}
}
