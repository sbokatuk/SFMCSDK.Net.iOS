//
// SFIdentity.Additions.cs
//
// Hand-written companion to the generated SFIdentity: the strongly typed Edit the binding
// definition cannot express. See the comment on _Edit in ApiDefinitions.cs for why the exported
// block is typed with plain NSObject.
//

using System;
using Foundation;
using ObjCRuntime;

namespace SFMCSDK
{
	public partial class SFIdentity
	{
		/// <summary>
		/// Edits the identity through the native modifier. The updater receives the modifier the
		/// SDK hands to the <c>editWithIdentity:</c> block and must return the modifier the SDK
		/// should apply - conventionally the same instance it was given.
		/// </summary>
		public void Edit (Func<ISFIdentityModifier, ISFIdentityModifier> updater)
		{
			if (updater is null)
				throw new ArgumentNullException (nameof (updater));

			_Edit (modifier =>
			{
				// The native modifier's class is not a registered managed type, so wrap its
				// handle in the generated protocol wrapper, whose members call the native
				// selectors. Wrapping the model class instead is what crashed on the .NET 10
				// managed static registrar - the model is abstract and its members do not call
				// into native code anyway.
				var typed = Runtime.GetINativeObject<ISFIdentityModifier> (modifier.Handle, false)
					?? throw new InvalidOperationException ("The native identity modifier could not be wrapped as ISFIdentityModifier.");

				var result = updater (typed)
					?? throw new InvalidOperationException ("The identity updater must return a modifier; the native block's return is non-null.");

				return result as NSObject
					?? Runtime.GetNSObject (result.Handle)
					?? throw new InvalidOperationException ("The returned identity modifier is not an NSObject.");
			});
		}
	}
}
