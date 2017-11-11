using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Kent.Boogaart.Truss.Fluent
{
	/// <summary>
	/// Used to hide irrelevant members in the fluent API to make Intellisense cleaner. Note that this only works for people referencing this assembly. It doesn't
	/// work within this solution.
	/// </summary>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public interface IHideObjectMembers
	{
		/// <summary/>
		[SuppressMessage("Microsoft.Design", "CA1024", Justification = "It's a method so we can hide it.")]
		[SuppressMessage("Microsoft.Naming", "CA1716", MessageId = "GetType", Justification = "Has same name so we can hide it.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		Type GetType();

		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		int GetHashCode();

		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		string ToString();

		/// <summary/>
		[EditorBrowsable(EditorBrowsableState.Never)]
		bool Equals(object other);
	}
}