using System;
using OrigoDB.Core;
using XmcdParser;

namespace FreeDb
{
    [Serializable]
    public class AddDiscCommand : Command<FreeDbModel>
    {
        public AddDiscCommand(Disc disc)
        {
            Disc = disc;
        }

        public readonly Disc Disc;

        public override void Execute(FreeDbModel model)
        {
            model.AddDisc(Disc);
        }
    }
}
