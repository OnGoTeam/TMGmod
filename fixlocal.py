import os
from uuid import uuid4
fold = "content\\maps"
a = os.listdir(fold)


def fix(fn: str):
    with open(fn, 'rb') as f:
        b = f.read()
    b = b.replace(b'hasLocalMods\x01\x00\x00\x00\x01\x0b\x00', b'hasLocalMods\x01\x00\x00\x00\x00\x0b\x00')
    i = b.find(b'guid&\x00\x00\x00$\x00') + 10
    b = b[:i] + str(uuid4()).encode() + b[i + 36:]
    i = b.find(b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01') + 20
    if b[i + 10:].startswith(b'proceduralData'):
        b = b[:i] + b'\x13\x03\xc1\x42' + b[i + 4:]
    # b = b.replace(b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x91\x03\x89\x72',
    #               b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x13\x03\xc1\x42')
    with open(fn, 'wb') as f:
        f.write(b)


for fnn in a:
    fix(os.path.join(fold, fnn))
