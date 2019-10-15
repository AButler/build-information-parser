using NUnit.Framework;

namespace BuildInformationParser.UnitTests {
  [TestFixture]
  public class TagNameParserTests {
    [TestCase( "refs/tags/v1.0.0", "v1.0.0" )]
    [TestCase( "refs/tags/v2.1.2-alpha001", "v2.1.2-alpha001" )]
    [TestCase( "refs/tags/a", "a" )]
    public void Valid( string input, string expected ) {
      var tagNameParser = new TagNameParser();

      var actual = tagNameParser.Parse( input );

      Assert.That( actual, Is.EqualTo( expected ) );
    }

    [Test]
    public void InvalidNull() {
      var tagNameParser = new TagNameParser();

      Assert.That( () => tagNameParser.Parse( null ), Throws.ArgumentNullException );
    }

    [TestCase( "refs/head/master" )]
    [TestCase( "refs/head/" )]
    [TestCase( "refs/tags/" )]
    [TestCase( "refs/tags/ " )]
    [TestCase( "master" )]
    [TestCase( "v1.0.0" )]
    [TestCase( "v1.0.0-alpha002" )]
    [TestCase( "refs/tags/v1.0.0 " )]
    public void Invalid( string input ) {
      var tagNameParser = new TagNameParser();

      Assert.That( () => tagNameParser.Parse( input ), Throws.ArgumentException );
    }
  }
}