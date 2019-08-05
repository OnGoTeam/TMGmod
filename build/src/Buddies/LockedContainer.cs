using DuckGame;

namespace TMGmod.Buddies
{
    public abstract class LockedContainer<T> : Holdable where T : Thing
    {
        protected abstract Vec2 SpawnPos { get; }
        public override void Touch(MaterialThing with)
        {
            base.Touch(with);
            Key key = null;
            if (with is Key || with is Duck duck1 && duck1.holdObject is Key)
            {
                if (with is Duck duck2 && duck2.holdObject is Key key1)
                    key = key1;
                if (with is Key key2)
                    key = key2;
                if (key == null) return; //For Clarity Sake
                var newThing = Editor.CreateThing(typeof(T));
                if (newThing == null) return;
                newThing.position = Offset(SpawnPos);
                Level.Add(newThing);
                Level.Remove(key);
                Level.Remove(this);
            }
        }
        public LockedContainer(float xval, float yval):base(xval, yval)
        {
        }

        public override ContextMenu GetContextMenu()
        {
            var editorGroupMenu1 = new EditorGroupMenu(null, true);
            if (_canFlip)
                editorGroupMenu1.AddItem(new ContextCheckBox("Flip", null, new FieldBinding(this, "flipHorizontal")));
            if (_canFlipVert)
                editorGroupMenu1.AddItem(new ContextCheckBox("Flip V", null, new FieldBinding(this, "flipVertical")));
            if (_canHaveChance)
            {
                var editorGroupMenu2 = new EditorGroupMenu(editorGroupMenu1)
                {
                    text = "Chance",
                    tooltip = "Likelyhood for this object to exist in the level."
                };
                editorGroupMenu1.AddItem(editorGroupMenu2);
                editorGroupMenu2.AddItem(new ContextSlider("Chance", null, new FieldBinding(this, "likelyhoodToExist"), 0.05f, null, false, null, "Chance for object to exist. 1.0 = 100% chance."));
                editorGroupMenu2.AddItem(new ContextSlider("Chance Group", null, new FieldBinding(this, "chanceGroup", -1f, 10f), 1f, null, false, null, "All objects in a chance group will exist, if their group's chance roll is met. -1 means no grouping."));
                editorGroupMenu2.AddItem(new ContextCheckBox("Accessible", null, new FieldBinding(this, "isAccessible"), null, "Flag for level generation, set this to false if the object is behind a locked door and not neccesarily accessible."));
            }

            if (sequence == null) return editorGroupMenu1;
            //else
            {
                var editorGroupMenu2 = new EditorGroupMenu(editorGroupMenu1)
                {
                    text = "Sequence"
                };
                editorGroupMenu1.AddItem(editorGroupMenu2);
                editorGroupMenu2.AddItem(new ContextCheckBox("Loop", null, new FieldBinding(sequence, "loop")));
                editorGroupMenu2.AddItem(new ContextSlider("Order", null, new FieldBinding(sequence, "order", 0.0f, 100f), 1f, "RAND"));
                editorGroupMenu2.AddItem(new ContextCheckBox("Wait", null, new FieldBinding(sequence, "waitTillOrder")));
            }
            return editorGroupMenu1;
        }
    }
}