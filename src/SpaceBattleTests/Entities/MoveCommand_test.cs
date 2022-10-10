namespace SpaceBattleTests.Entities;
using SpaceBattleTests.Attributes;

using SpaceBattle.Entities;
using SpaceBattle.Base;

using System;

using Moq;

public class MoveCommandTests
{
    [Theory(Timeout = 1000)]
    [InlineData(0, 0, 0, 0)]
    [InlineData(0, 0, 1, 2)]
    [InlineData(0, 0, -1, -2)]
    [InlineData(0, 0, -1, 2)]
    [InlineData(0, 0, 1, -2)]
    [InlineData(1, 2, 0, 0)]
    [InlineData(-1, 2, 0, 0)]
    [InlineData(1, -2, 0, 0)]
    [InlineData(-1, -2, 0, 0)]
    [InlineData(1, 2, -2, -1)]
    [InlineData(-3, -4, 1, 2)]
    [InlineData(12, 5, -7, 3)]
    public void SuccessfulMoveTests(int pos_x, int pos_y, int mov_x, int mov_y)
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { pos_x, pos_y });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { mov_x, mov_y });

        var Mover = new MoveCommand(Mock.Object);

        // Action
        Mover.Run();

        // Assertation
        Assert.Equal(new int[] { pos_x + mov_x, pos_y + mov_y }, Mock.Object.Position);
    }

    [Theory(Timeout = 1000)]
    [Repeat(50)]
    public void RandomSuccessfulMoveTests(int _)
    {

        // Initialization
        Random rand = new Random();

        int[] position = new int[]{rand.Next(int.MinValue, int.MaxValue),
                               rand.Next(int.MinValue, int.MaxValue)};

        int[] moveSpeed = new int[]{rand.Next(int.MinValue, int.MaxValue),
                               rand.Next(int.MinValue, int.MaxValue)};

        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, position.Clone());
        Mock.SetupGet(mov => mov.MoveSpeed).Returns((int[])moveSpeed.Clone());

        var Mover = new MoveCommand(Mock.Object);

        // Action
        Mover.Run();

        // Assertation
        int[] Expected = new int[] { position[0] + moveSpeed[0], position[1] + moveSpeed[1] };
        Assert.Equal(Expected, Mock.Object.Position);
    }

    [Fact]
    public void ZeroSizeVectorsTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { });

        var Mover = new MoveCommand(Mock.Object);

        // Action
        Mover.Run();

        // Assertation
        Assert.Equal(new int[] { }, Mock.Object.Position);
    }

    [Fact(Timeout = 1000)]
    public void NullPositionMoveTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, null);
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { 0, 0 });

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }
    [Fact(Timeout = 1000)]
    public void NullMoveSpeedTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { 0, 0 });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns((int[]?)null!);

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }

    [Fact(Timeout = 1000)]
    public void NullMoveSpeedAndPositionTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, null);
        Mock.SetupGet(mov => mov.MoveSpeed).Returns((int[]?)null!);

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }

    [Fact(Timeout = 1000)]
    public void MoveSpeedSizeMoreThenPositionSizeTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { 1, 2, 3 });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { 1, 2 });

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }

    [Fact(Timeout = 1000)]
    public void PositionSizeMoreThenMoveSpeedSizeTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { 1, 2 });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { 1, 2, 3 });

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }

    [Fact(Timeout = 1000)]
    public void PositionSizeIsZeroTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { 1, 2, 3 });

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }

    [Fact(Timeout = 1000)]
    public void MoveSpeedSizeIsZeroTest()
    {
        // Prepare
        var Mock = new Mock<IMovable>();
        Mock.SetupProperty(mov => mov.Position, new int[] { 1, 2 });
        Mock.SetupGet(mov => mov.MoveSpeed).Returns(new int[] { });

        var Mover = new MoveCommand(Mock.Object);

        // Assertation
        Assert.Throws<Exception>(() => Mover.Run());
    }
}
