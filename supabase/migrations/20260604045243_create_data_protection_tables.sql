/*
  # Create Data Protection Management Tables

  ## Overview
  Sets up the data protection and access control entities for fine-grained data authorization.

  ## New Tables

  ### `dp_entity_type_infos`
  Registers entity types that are subject to data protection rules.
  - `id` (uuid, PK)
  - `name` (varchar 64) - Short name, indexed
  - `display_name` (varchar 128)
  - `type_full_name` (varchar 256) - .NET fully-qualified type name, indexed
  - `is_audit_enabled` (bool) - Whether access is audited
  - Audit fields

  ### `dp_entity_property_infos`
  Properties of a protected entity that can be used in filter rules.
  - `id` (uuid, PK)
  - `entity_type_id` (uuid, FK → dp_entity_type_infos)
  - `name` (varchar 64)
  - `display_name` (varchar 128)
  - `type_full_name` (varchar 256) - Property .NET type
  - `is_nullable` (bool)
  - Audit fields

  ### `dp_entity_enum_infos`
  Enum value definitions for enum-typed entity properties.
  - `id` (uuid, PK)
  - `entity_property_id` (uuid, FK → dp_entity_property_infos)
  - `name` (varchar 64)
  - `display_name` (varchar 128)
  - `value` (varchar 64) - Enum numeric or string value
  - Audit fields

  ### `dp_role_entity_rules`
  Data access filter rules assigned to roles.
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - `entity_type_id` (uuid, FK → dp_entity_type_infos)
  - `entity_type_full_name` (varchar 256)
  - `role_id` (uuid)
  - `role_name` (varchar 256) - Indexed
  - `is_enabled` (bool)
  - `operation` (smallint) - DataAccessOperation enum
  - `filter_group` (jsonb) - DataAccessFilterGroup structure
  - `accessed_properties` (varchar 512, nullable)
  - Audit fields

  ### `dp_organization_unit_entity_rules`
  Data access filter rules assigned to organization units.
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - `entity_type_id` (uuid, FK → dp_entity_type_infos)
  - `entity_type_full_name` (varchar 256)
  - `org_id` (uuid)
  - `org_code` (varchar 128) - Organization unit code
  - `is_enabled` (bool)
  - `operation` (smallint)
  - `filter_group` (jsonb)
  - `accessed_properties` (varchar 512, nullable)
  - Audit fields

  ### `dp_subject_strategies`
  Access strategy settings per subject (user/role/org).
  - `id` (uuid, PK)
  - `tenant_id` (uuid, nullable)
  - `is_enabled` (bool) - Default true
  - `subject_name` (varchar 30) - Subject type name
  - `subject_id` (varchar 64) - Unique identifier of the subject
  - `strategy` (smallint) - DataAccessStrategy enum
  - `extra_properties` (jsonb)
  - Audit fields

  ## Security
  - RLS enabled on all tables
  - Only authenticated users can read/manage records

  ## Notes
  - `filter_group` stores complex JSON filter trees (groups of rules with AND/OR logic)
  - `operation` values: 0=Read, 1=Write, 2=Delete (DataAccessOperation enum)
  - `strategy` values: 0=Default, 1=Allow, 2=Deny (DataAccessStrategy enum)
*/

-- Entity Type Infos
CREATE TABLE IF NOT EXISTS dp_entity_type_infos (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  type_full_name varchar(256) NOT NULL,
  is_audit_enabled boolean DEFAULT true,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_dp_entity_type_infos_name ON dp_entity_type_infos(name);
CREATE INDEX IF NOT EXISTS idx_dp_entity_type_infos_type_full_name ON dp_entity_type_infos(type_full_name);

ALTER TABLE dp_entity_type_infos ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read dp_entity_type_infos"
  ON dp_entity_type_infos FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert dp_entity_type_infos"
  ON dp_entity_type_infos FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update dp_entity_type_infos"
  ON dp_entity_type_infos FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete dp_entity_type_infos"
  ON dp_entity_type_infos FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Entity Property Infos
CREATE TABLE IF NOT EXISTS dp_entity_property_infos (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  entity_type_id uuid NOT NULL REFERENCES dp_entity_type_infos(id),
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  type_full_name varchar(256) NOT NULL,
  is_nullable boolean DEFAULT false,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_dp_entity_property_infos_entity_type_id ON dp_entity_property_infos(entity_type_id);

ALTER TABLE dp_entity_property_infos ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read dp_entity_property_infos"
  ON dp_entity_property_infos FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert dp_entity_property_infos"
  ON dp_entity_property_infos FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update dp_entity_property_infos"
  ON dp_entity_property_infos FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete dp_entity_property_infos"
  ON dp_entity_property_infos FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Entity Enum Infos
CREATE TABLE IF NOT EXISTS dp_entity_enum_infos (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  entity_property_id uuid NOT NULL REFERENCES dp_entity_property_infos(id),
  name varchar(64) NOT NULL,
  display_name varchar(128) NOT NULL,
  value varchar(64) NOT NULL,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_dp_entity_enum_infos_property_id ON dp_entity_enum_infos(entity_property_id);

ALTER TABLE dp_entity_enum_infos ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read dp_entity_enum_infos"
  ON dp_entity_enum_infos FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert dp_entity_enum_infos"
  ON dp_entity_enum_infos FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update dp_entity_enum_infos"
  ON dp_entity_enum_infos FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete dp_entity_enum_infos"
  ON dp_entity_enum_infos FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Role Entity Rules
CREATE TABLE IF NOT EXISTS dp_role_entity_rules (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  entity_type_id uuid NOT NULL REFERENCES dp_entity_type_infos(id),
  entity_type_full_name varchar(256) NOT NULL,
  role_id uuid NOT NULL,
  role_name varchar(256) NOT NULL,
  is_enabled boolean DEFAULT true,
  operation smallint DEFAULT 0,
  filter_group jsonb DEFAULT '{}'::jsonb,
  accessed_properties varchar(512),
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_dp_role_entity_rules_entity_type_id ON dp_role_entity_rules(entity_type_id);
CREATE INDEX IF NOT EXISTS idx_dp_role_entity_rules_role_name ON dp_role_entity_rules(role_name);
CREATE INDEX IF NOT EXISTS idx_dp_role_entity_rules_tenant_id ON dp_role_entity_rules(tenant_id);

ALTER TABLE dp_role_entity_rules ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read dp_role_entity_rules"
  ON dp_role_entity_rules FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert dp_role_entity_rules"
  ON dp_role_entity_rules FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update dp_role_entity_rules"
  ON dp_role_entity_rules FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete dp_role_entity_rules"
  ON dp_role_entity_rules FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Organization Unit Entity Rules
CREATE TABLE IF NOT EXISTS dp_org_unit_entity_rules (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  entity_type_id uuid NOT NULL REFERENCES dp_entity_type_infos(id),
  entity_type_full_name varchar(256) NOT NULL,
  org_id uuid NOT NULL,
  org_code varchar(128) NOT NULL,
  is_enabled boolean DEFAULT true,
  operation smallint DEFAULT 0,
  filter_group jsonb DEFAULT '{}'::jsonb,
  accessed_properties varchar(512),
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_dp_org_unit_entity_rules_entity_type_id ON dp_org_unit_entity_rules(entity_type_id);
CREATE INDEX IF NOT EXISTS idx_dp_org_unit_entity_rules_org_id ON dp_org_unit_entity_rules(org_id);
CREATE INDEX IF NOT EXISTS idx_dp_org_unit_entity_rules_tenant_id ON dp_org_unit_entity_rules(tenant_id);

ALTER TABLE dp_org_unit_entity_rules ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read dp_org_unit_entity_rules"
  ON dp_org_unit_entity_rules FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert dp_org_unit_entity_rules"
  ON dp_org_unit_entity_rules FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update dp_org_unit_entity_rules"
  ON dp_org_unit_entity_rules FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete dp_org_unit_entity_rules"
  ON dp_org_unit_entity_rules FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);


-- Subject Strategies
CREATE TABLE IF NOT EXISTS dp_subject_strategies (
  id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
  tenant_id uuid,
  is_enabled boolean DEFAULT true,
  subject_name varchar(30) NOT NULL,
  subject_id varchar(64) NOT NULL,
  strategy smallint DEFAULT 0,
  extra_properties jsonb DEFAULT '{}'::jsonb,
  creation_time timestamptz DEFAULT now(),
  creator_id uuid,
  last_modification_time timestamptz,
  last_modifier_id uuid
);

CREATE INDEX IF NOT EXISTS idx_dp_subject_strategies_subject_name ON dp_subject_strategies(subject_name);
CREATE INDEX IF NOT EXISTS idx_dp_subject_strategies_subject_id ON dp_subject_strategies(subject_id);
CREATE INDEX IF NOT EXISTS idx_dp_subject_strategies_tenant_id ON dp_subject_strategies(tenant_id);

ALTER TABLE dp_subject_strategies ENABLE ROW LEVEL SECURITY;

CREATE POLICY "Authenticated users can read dp_subject_strategies"
  ON dp_subject_strategies FOR SELECT
  TO authenticated
  USING (true);

CREATE POLICY "Authenticated users can insert dp_subject_strategies"
  ON dp_subject_strategies FOR INSERT
  TO authenticated
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can update dp_subject_strategies"
  ON dp_subject_strategies FOR UPDATE
  TO authenticated
  USING (auth.uid() IS NOT NULL)
  WITH CHECK (auth.uid() IS NOT NULL);

CREATE POLICY "Authenticated users can delete dp_subject_strategies"
  ON dp_subject_strategies FOR DELETE
  TO authenticated
  USING (auth.uid() IS NOT NULL);
