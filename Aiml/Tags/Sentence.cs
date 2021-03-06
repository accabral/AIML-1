using System.Text;
using System.Xml;

namespace Aiml {
	public partial class TemplateNode {
		/// <summary>
		///     Converts the content to sentence case.
		/// </summary>
		/// <remarks>
		///     This element capitalises the first letter of each sentence of the content.
		///     This element is defined by the AIML 1.1 specification.
		/// </remarks>
		public sealed class Sentence : RecursiveTemplateTag {
			public Sentence(TemplateElementCollection children) : base(children) { }

			public override string Evaluate(RequestProcess process) {
				var value = new StringBuilder(this.Children?.Evaluate(process) ?? "");

				int i;
				for (i = 0; i < value.Length; ++i) {
					if (char.IsLetterOrDigit(value[i])) {
						if (char.IsLower(value[i])) value[i] = char.ToUpper(value[i]);
						break;
					}
				}
				for (++i; i < value.Length; ++i)
					if (char.IsUpper(value[i])) value[i] = char.ToLower(value[i]);

				return value.ToString();
			}

			public static Sentence FromXml(XmlNode node, AimlLoader loader) {
				return new Sentence(TemplateElementCollection.FromXml(node, loader));
			}
		}
	}
}
