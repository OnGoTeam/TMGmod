import os
from pathlib import Path
from uuid import uuid4


mode = input()
did = b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x91\x03\x89\x72'
pid = b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x13\x03\xc1\x42'
sid = b'workshopIDs\r\x00\x00\x00\x01\x00\x00\x00\x01\x9b\xe9\x19\xaa'
if mode != 'dev':
    wid = { 'prod': pid, 'stgn': sid }[mode]


def fix(fn: str):
    with open(fn, 'rb') as f:
        b = f.read()
    if mode == 'dev':
        b = b.replace(pid, did)
        b = b.replace(sid, did)
    else:
        b = b.replace(did, wid)
    with open(fn, 'wb') as f:
        f.write(b)


for path in Path('content/TMG Levels').glob('*.lev'):
    fix(path)
print('done')
input()
