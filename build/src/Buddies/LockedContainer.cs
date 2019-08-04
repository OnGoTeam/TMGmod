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
            if (with is Key || with is Duck duck && duck.holdObject is Key)
            {
                if (with is Duck duck1 && duck1.holdObject is Key key1)
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
            EditorGroupMenu editorGroupMenu1 = new EditorGroupMenu(null, true, null);
            if (_canFlip)
                editorGroupMenu1.AddItem(new ContextCheckBox("Flip", null, new FieldBinding(this, "flipHorizontal", 0.0f, 1f, 0.1f), null));
            if (_canFlipVert)
                editorGroupMenu1.AddItem(new ContextCheckBox("Flip V", null, new FieldBinding(this, "flipVertical", 0.0f, 1f, 0.1f), null));
            if (_canHaveChance)
            {
                EditorGroupMenu editorGroupMenu2 = new EditorGroupMenu(editorGroupMenu1, false, null)
                {
                    text = "Chance",
                    tooltip = "Likelyhood for this object to exist in the level."
                };
                editorGroupMenu1.AddItem(editorGroupMenu2);
                editorGroupMenu2.AddItem(new ContextSlider("Chance", null, new FieldBinding(this, "likelyhoodToExist", 0.0f, 1f, 0.1f), 0.05f, null, false, null, "Chance for object to exist. 1.0 = 100% chance."));
                editorGroupMenu2.AddItem(new ContextSlider("Chance Group", null, new FieldBinding(this, "chanceGroup", -1f, 10f, 0.1f), 1f, null, false, null, "All objects in a chance group will exist, if their group's chance roll is met. -1 means no grouping."));
                editorGroupMenu2.AddItem(new ContextCheckBox("Accessible", null, new FieldBinding(this, "isAccessible", 0.0f, 1f, 0.1f), null, "Flag for level generation, set this to false if the object is behind a locked door and not neccesarily accessible."));
            }
            if (sequence != null)
            {
                EditorGroupMenu editorGroupMenu2 = new EditorGroupMenu(editorGroupMenu1, false, null)
                {
                    text = "Sequence"
                };
                editorGroupMenu1.AddItem(editorGroupMenu2);
                editorGroupMenu2.AddItem(new ContextCheckBox("Loop", null, new FieldBinding(sequence, "loop", 0.0f, 1f, 0.1f), null));
                editorGroupMenu2.AddItem(new ContextSlider("Order", null, new FieldBinding(sequence, "order", 0.0f, 100f, 0.1f), 1f, "RAND", false, null));
                editorGroupMenu2.AddItem(new ContextCheckBox("Wait", null, new FieldBinding(sequence, "waitTillOrder", 0.0f, 1f, 0.1f), null));
            }
            return editorGroupMenu1;
        }
    }
}