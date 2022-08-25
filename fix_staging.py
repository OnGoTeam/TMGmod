import os
from pathlib import Path
from uuid import uuid4


wid = {
    'prod': b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x13\x03\xc1\x42',
    'stgn': b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x9b\xe9\x19\xaa',
}[input()]


def fix(fn: str):
    with open(fn, 'rb') as f:
        b = f.read()
    b = b.replace(
        b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x91\x03\x89\x72',
        wid
    )
    with open(fn, 'wb') as f:
        f.write(b)


for path in Path('content/TMG Levels').glob('*.lev'):
    fix(path)
print('done')
input()
