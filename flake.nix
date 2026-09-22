{
  description = "Common Wealth Bank Server";

  inputs = {
    nixpkgs.url = "github:NixOS/nixpkgs/nixos-unstable";
  };

  outputs = { nixpkgs, ... }:
    let
      system = "x86_64-linux";
      pkgs = nixpkgs.legacyPackages.${system};
    in
    {
      devShells.${system}.default = pkgs.mkShell {
        packages = with pkgs; [
          (with pkgs.dotnetCorePackages;
          combinePackages [
            sdk_9_0
            aspnetcore_9_0
          ])
          omnisharp-roslyn
          csharpier
        ];
      };
    };
}
