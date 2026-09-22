# Resterm quickstart

1. Run `resterm` in this directory.
2. Press `g Shift+M` to start the local mock server.
3. Open `requests.http`, place the cursor inside a request, then press Ctrl+Enter.
4. Press Ctrl+E to switch between the local `dev` and `test` environments.
5. Edit `resterm.env.json` or copy from `resterm.env.example.json`.

For CLI runs, start `resterm mock requests.http` in one terminal, then run requests from another.

Next steps:
- The mock scenarios demonstrate JSON matching, numeric rules, and response interpolation.
- The requests demonstrate assertions, bearer auth, and two forms of `@for-each`.
- `rts/helpers.rts` shows object lists, `??`, and the ternary operator.
- See docs in [docs/resterm.md](https://github.com/unkn0wn-root/resterm/blob/main/docs/resterm.md) for details.
